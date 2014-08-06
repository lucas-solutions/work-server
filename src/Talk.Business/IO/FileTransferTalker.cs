using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    /// <summary>
    /// FTP Talker
    /// </summary>
    public class FileTransferTalker : Talker
    {
        public override TalkProtocol Protocol
        {
            get { return TalkProtocol.FTP; }
        }

        public override Task PullAsync(TalkContext context, Stream stream)
        {
            throw new NotImplementedException();
        }

        public override Task PushAsync(TalkContext context, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
