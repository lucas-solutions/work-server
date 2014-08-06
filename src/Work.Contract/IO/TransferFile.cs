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

        public abstract Task PushAsync(Stream stream, Action<float> progress);
    }
}
