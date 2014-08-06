using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public class TalkProtocol
    {
        /// <summary>
        /// Applicability Statement 2
        /// </summary>
        public static readonly TalkProtocol AS2 = new TalkProtocol("AS2", TalkFormat.Binary);

        /// <summary>
        /// DropBox
        /// </summary>
        public static readonly TalkProtocol DropBox = new TalkProtocol("DropBox", TalkFormat.Binary);

        /// <summary>
        /// Facebook Protocol/Etiquette
        /// </summary>
        public static readonly TalkProtocol Facebook = new TalkProtocol("Facebook", TalkFormat.Xml | TalkFormat.Json);

        /// <summary>
        /// Local File System.
        /// </summary>
        public static readonly TalkProtocol FileSystem = new TalkProtocol("FileSystem", TalkFormat.Binary);

        /// <summary>
        /// File Transfer Protocol
        /// </summary>
        public static readonly TalkProtocol FTP = new TalkProtocol("FTP", TalkFormat.Binary);

        /// <summary>
        /// Google Drive
        /// </summary>
        public static readonly TalkProtocol GoogleDrive = new TalkProtocol("GoogleDrive", TalkFormat.Binary);

        /// <summary>
        /// Loggly
        /// </summary>
        public static readonly TalkProtocol Loggly = new TalkProtocol("Loggly", TalkFormat.Json | TalkFormat.Text);

        /// <summary>
        /// Log Stash
        /// </summary>
        public static readonly TalkProtocol LogStash = new TalkProtocol("LogStash", TalkFormat.Json | TalkFormat.Text);

        /// <summary>
        /// Amazon S3
        /// </summary>
        public static readonly TalkProtocol S3 = new TalkProtocol("S3", TalkFormat.Binary);

        /// <summary>
        /// Secure File Transfer Protocol
        /// </summary>
        public static readonly TalkProtocol SFTP = new TalkProtocol("SFTP", TalkFormat.Binary);

        public static implicit operator string(TalkProtocol protocol)
        {
            return protocol != null ? protocol._name : null;
        }

        private readonly string _name;
        private readonly TalkFormat _format;
        
        private TalkProtocol(string name, TalkFormat format)
        {
            _name = name;
            _format = format;
        }

        public string Name
        {
            get { return _name; }
        }

        public TalkFormat Format
        {
            get { return _format; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
