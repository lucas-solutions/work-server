using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public abstract class TransferFile
    {
        private Guid _id = Guid.NewGuid();

        public DateTimeOffset? CreatedOn
        {
            get;
            protected set;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public DateTimeOffset? ModifiedOn
        {
            get;
            protected set;
        }

        /// <summary>
        /// Path relative to directory
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Size in bytes
        /// </summary>
        public long? Size
        {
            get;
            set;
        }

        protected virtual void Copy(Stream inputStream, Stream outputStream, Action<float> progress)
        {
            var buffer = new byte[0x1000];
            int count, position = 0;
            for (; buffer.Length == (count = inputStream.Read(buffer, 0, buffer.Length)); position += count)
                outputStream.Write(buffer, 0, count);
            outputStream.Write(buffer, 0, count);
            position += count;
        }

        public abstract void Read(Stream outputStream, Action<float> progress);

        public abstract void Write(Stream inputStream, Action<float> progress);
    }
}
