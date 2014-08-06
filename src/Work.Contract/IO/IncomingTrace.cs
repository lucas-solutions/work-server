using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class IncomingTrace : TransferTrace
    {
        public OutgoingTrace Sender
        {
            get;
            set;
        }

        public int SenderId
        {
            get;
            set;
        }
    }
}
