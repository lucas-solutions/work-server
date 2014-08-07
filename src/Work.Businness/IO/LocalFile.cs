using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class LocalFile : TransferFile
    {
        public LocalFile(string relativePath, LocalDirectory directory)
        {
            Directory = directory;
            Path = relativePath;
        }

        public LocalDirectory Directory
        {
            get;
            private set;
        }

        public override void Read(Stream outputStream, Action<float> progress)
        {
            var info = new FileInfo(System.IO.Path.Combine(Directory.Path, Path));

            CreatedOn = info.CreationTime;
            ModifiedOn = info.LastWriteTime;
            Size = info.Length;

            var inputStream = info.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            Copy(inputStream, outputStream, progress);
            inputStream.Close();
        }

        public override void Write(Stream inputStream, Action<float> progress)
        {
            var info = new FileInfo(System.IO.Path.Combine(Directory.Path, Path));
            
            var outputStream = info.Open(FileMode.Create, FileAccess.Write, FileShare.None);
            Copy(inputStream, outputStream, progress);
            outputStream.Flush();
            outputStream.Close();

            CreatedOn = info.CreationTime;
            ModifiedOn = info.LastWriteTime;
            Size = info.Length;
        }
    }
}
