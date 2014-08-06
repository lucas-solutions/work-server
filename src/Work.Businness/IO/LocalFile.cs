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
        private readonly LocalDirectory _directory;

        public LocalFile(FileInfo info, LocalDirectory directory)
        {
            _directory = directory;
            Path = info.FullName.Substring(info.FullName.Length);
            CreatedOn = info.CreationTime;
            ModifiedOn = info.LastWriteTime;
            Size = info.Length;
        }

        public LocalDirectory Directory
        {
            get { return _directory; }
        }

        public override Task PushAsync(Stream stream, Action<float> progress)
        {
            return new Task(() =>
            {

            });
        }
    }
}
