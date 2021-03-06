﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Automation;
    using System.Configuration;
    using System.IO;

    public class TransferWorker : ITransferWorker
    {
        private DirectoryInfo _temporal = null;
        private Transfer _transfer;
        private ICollection<TransferDirectory> _directories;


        protected ICollection<TransferDirectory> Directories
        {
            get
            {
                return _directories ?? (_directories = Array.AsReadOnly(Transfer.Parties
                    .Select(party => Resolve(party))
                    .ToArray()));
            }
        }

        public float Progress
        {
            get;
            protected set;
        }

        Task IWork.Task
        {
            get { return _transfer; }
            set { _transfer = value as Transfer; }
        }

        Transfer IWork<Transfer>.Task
        {
            get { return _transfer; }
            set { _transfer = value as Transfer; }
        }

        public DirectoryInfo Temporal
        {
            get
            {
                if (_temporal != null) return _temporal;

                var path = ConfigurationManager.AppSettings.Get("temporal");

                if (path == null)
                    path = Path.Combine(Directory.GetCurrentDirectory(), ".tmp");
                else if (!Path.IsPathRooted(path))
                    path = Path.Combine(Directory.GetCurrentDirectory(), path);

                _temporal = new DirectoryInfo(path);

                if (!_temporal.Exists)
                {
                    _temporal.Create();
                }

                return _temporal;
            }
        }

        public virtual Transfer Transfer
        {
            get { return _transfer; }
            set { _transfer = value; }
        }

        protected virtual TransferDirectory Resolve(Party party)
        {
            switch (party.Host.Protocol)
            {
                case HostProtocol.FileTransfer:
                    return new RemoteDirectory(party);

                case HostProtocol.FileSystem:
                    return new LocalDirectory(party);

                default:
                    // unsuported;
                    break;
            }

            return null;
        }

        public void Work()
        {
            // collection
            var outgoingTasks = Read();

            // distribution
            var incomingTasks = Write(outgoingTasks);
        }

        public System.Threading.Tasks.Task WorkAsync()
        {
            return System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    // collection
                    var outgoingTasks = ReadAsync();

                    // distribution
                    var incomingTasks = WriteAsync(outgoingTasks);

                    for (var count = 0; count < incomingTasks.Length; count++)
                    {
                        var index = System.Threading.Tasks.Task.WaitAny(incomingTasks);

                        var completed = incomingTasks[index].Result;
                    }

                    System.Threading.Tasks.Task.WhenAll(incomingTasks);
                });
        }

        protected virtual OutgoingTrace[] Read()
        {
            var localDirectories = Transfer.Parties
                    .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileSystem)
                    .Select(party => new LocalDirectory(party))
                    .ToArray();

            var remoteDirectories = Transfer.Parties
                    .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileTransfer)
                    .Select(party => new RemoteDirectory(party))
                    .ToArray();

            var localFiles = localDirectories.SelectMany(dir => dir.GetFiles());

            var remoteFiles = remoteDirectories.SelectMany(dir => dir.GetFiles());

            return Read(localFiles).Union(Read(remoteFiles)).ToArray();
        }

        protected virtual OutgoingTrace[] Read(IEnumerable<LocalFile> files)
        {
            var trances = new List<OutgoingTrace>();

            foreach (var file in files)
            {
                trances.Add(Read(file, file.Directory.Party));
            }

            return trances.ToArray();
        }

        protected virtual OutgoingTrace[] Read(IEnumerable<RemoteFile> files)
        {
            var trances = new List<OutgoingTrace>();

            foreach (var file in files)
            {
                trances.Add(Read(file, file.Directory.Party));
            }

            return trances.ToArray();
        }

        protected virtual OutgoingTrace Read(TransferFile file, Party party)
        {
            var outgoing = new OutgoingTrace
            {
                File = file.Path,
                Party = party,
                PartyId = party.Id,
                Size = file.Size ?? -1,
                Transfer = Transfer,
                TransferId = Transfer.Id
            };

            var temporalName = Guid.NewGuid().ToString("N");
            var temporalPath = Path.Combine(Temporal.FullName, temporalName);

            try
            {
                var outputStream = File.Open(temporalPath, FileMode.Create, FileAccess.Write, FileShare.None);
                file.Read(outputStream, (progress) => { });
                outputStream.Flush();
                outputStream.Close();
                outgoing.Success = true;
                outgoing.Message = temporalName;
            }
            catch (Exception e)
            {
                outgoing.Success = false;
                outgoing.Message = e.Message;
            }

            return outgoing;
        }

        protected virtual Task<OutgoingTrace>[] ReadAsync()
        {
            var localDirectories = Transfer.Parties
                    .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileSystem)
                    .Select(party => new LocalDirectory(party))
                    .ToArray();

            var remoteDirectories = Transfer.Parties
                    .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileTransfer)
                    .Select(party => new RemoteDirectory(party))
                    .ToArray();

            var localTasks = localDirectories.Select(dir => Task<ICollection<LocalFile>>.Factory.StartNew(() => dir.GetFiles())).ToArray();

            // better synchronous for now
            //var remoteTasks = remoteDirectories.Select(dir => Task<ICollection<RemoteFile>>.Factory.StartNew(() => dir.GetFiles())).ToArray();
            var remoteTasks = remoteDirectories.Select(dir => new Task<ICollection<RemoteFile>>(() => dir.GetFiles())).ToArray();

            System.Threading.Tasks.Task.Factory.StartNew(() => StartInSequence(remoteTasks));

            return ReadAsync(localTasks).Union(ReadAsync(remoteTasks)).ToArray();
        }

        protected virtual Task<OutgoingTrace>[] ReadAsync(Task<ICollection<LocalFile>>[] outgoingTasks)
        {
            var tasks = new List<Task<OutgoingTrace>>();

            for (var count = 0; count < outgoingTasks.Length; count++)
            {
                var index = System.Threading.Tasks.Task.WaitAny(outgoingTasks);

                foreach (var file in outgoingTasks[index].Result)
                {
                    tasks.Add(Task<OutgoingTrace>.Factory.StartNew(() =>
                    {
                        return Read(file, file.Directory.Party);
                    }));
                }
            }

            return tasks.ToArray();
        }

        protected virtual Task<OutgoingTrace>[] ReadAsync(Task<ICollection<RemoteFile>>[] outgoingTasks)
        {
            var tasks = new List<Task<OutgoingTrace>>();

            for (var count = 0; count < outgoingTasks.Length; count++)
            {
                var index = System.Threading.Tasks.Task.WaitAny(outgoingTasks);

                foreach (var file in outgoingTasks[index].Result)
                {
                    tasks.Add(Task<OutgoingTrace>.Factory.StartNew(() =>
                        {
                            return Read(file, file.Directory.Party);
                        }));
                }
            }

            return tasks.ToArray();
        }

        protected virtual async void StartInSequence(IEnumerable<System.Threading.Tasks.Task> tasks)
        {
            foreach (var task in tasks)
            {
                task.Start();
                await task;
            }
        }

        protected virtual IncomingTrace[] Write(OutgoingTrace[] outgoing)
        {
            var incoming = new List<IncomingTrace>();

            var localDirectories = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.In && party.Host.Protocol == HostProtocol.FileSystem)
                .Select(party => new LocalDirectory(party))
                .ToArray();

            var remoteDirectories = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.In && party.Host.Protocol == HostProtocol.FileTransfer)
                .Select(party => new RemoteDirectory(party))
                .ToArray();

            foreach (var trace in outgoing)
            {
                incoming.AddRange(localDirectories.Select(dir => Write(trace, dir)));
                incoming.AddRange(remoteDirectories.Select(dir => Write(trace, dir)));
            }

            return incoming.ToArray();
        }

        protected virtual IncomingTrace Write(OutgoingTrace outgoing, LocalDirectory directory)
        {
            var file = new LocalFile(outgoing.File, directory);
            return Write(outgoing, file, directory.Party);
        }

        protected virtual IncomingTrace Write(OutgoingTrace outgoing, RemoteDirectory directory)
        {
            var file = new RemoteFile(outgoing.File, directory);
            var trace = Write(outgoing, file, directory.Party);
            System.Threading.Thread.SpinWait(100);
            return trace;
        }

        protected virtual IncomingTrace Write(OutgoingTrace outgoing, TransferFile file, Party party)
        {
            var incoming = new IncomingTrace
            {
                File = file.Path,
                Party = party,
                PartyId = party.Id,
                Sender = outgoing,
                Size = file.Size ?? -1,
                Transfer = Transfer,
                TransferId = Transfer.Id
            };

            outgoing.Recipients.Add(incoming);

            var temporalName = outgoing.Message;
            var temporalPath = Path.Combine(Temporal.FullName, temporalName);

            try
            {
                var inputStream = File.Open(temporalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                file.Write(inputStream, (progress) => { });
                inputStream.Flush();
                inputStream.Close();
                incoming.Success = true;
                incoming.Message = temporalName;
                Console.WriteLine("Success: {0}", file.Path);
            }
            catch (Exception e)
            {
                incoming.Success = false;
                incoming.Message = e.Message;
                Console.WriteLine("Error: {0}, {1}", file.Path, e.Message);
            }

            return incoming;
        }

        protected virtual Task<IncomingTrace>[] WriteAsync(Task<OutgoingTrace>[] outgoingTasks)
        {
            var tasks = new List<Task<IncomingTrace>>();

            var localDirectories = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.In && party.Host.Protocol == HostProtocol.FileSystem)
                .Select(party => new LocalDirectory(party))
                .ToArray();

            var remoteDirectories = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.In && party.Host.Protocol == HostProtocol.FileTransfer)
                .Select(party => new RemoteDirectory(party))
                .ToArray();

            for (var count = 0; count < outgoingTasks.Length; count++)
            {
                var index = System.Threading.Tasks.Task.WaitAny(outgoingTasks);

                var outgoing = outgoingTasks[index].Result;

                // local tasks are already started
                var localTasks = localDirectories.Select(dir => WriteAsync(outgoing, dir));

                // remote tasks are not started
                var remoteTasks = remoteDirectories.Select(dir => WriteAsync(outgoing, dir));

                tasks.AddRange(localTasks);
                tasks.AddRange(remoteTasks);

                // start remote tasks synchronous
                System.Threading.Tasks.Task.Factory.StartNew(() => StartInSequence(remoteTasks));
            }

            return tasks.ToArray();
        }

        protected virtual Task<IncomingTrace> WriteAsync(OutgoingTrace outgoing, LocalDirectory directory)
        {
            return Task<IncomingTrace>.Factory.StartNew(() =>
                {
                    var file = new LocalFile(outgoing.File, directory);
                    return Write(outgoing, file, directory.Party);
                });
        }

        protected virtual Task<IncomingTrace> WriteAsync(OutgoingTrace outgoing, RemoteDirectory directory)
        {
            return new Task<IncomingTrace>(() =>
            {
                var file = new RemoteFile(outgoing.File, directory);
                return Write(outgoing, file, directory.Party);
            });
        }
    }
}
