using System;
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
        private DirectoryInfo _temporal;
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

        Transfer IWorker<Transfer>.Task
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
            var remoteOutgoing = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileTransfer)
                .Select(party => new RemoteDirectory(party))
                .ToArray();

            var localOutgoing = Transfer.Parties
                .Where(party => party.Direction == TransferDirection.Out && party.Host.Protocol == HostProtocol.FileSystem)
                .Select(party => new LocalDirectory(party))
                .ToArray();

            var remoteFiles = remoteOutgoing.AsParallel().SelectMany(dir => dir.GetFiles());

            foreach (var file in remoteFiles)
            {
                var path = Path.Combine(Temporal.FullName, file.Id.ToString());
                file.PullAsync(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None), (progress) => { });
            }

            //var outgoingFiles = outgoingDir.AsParallel().SelectMany(dir => dir.GetFiles());
        }
    }
}
