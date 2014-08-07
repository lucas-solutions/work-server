using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Automation;

    public class OutgoingTrace : TransferTrace
    {
        private ICollection<IncomingTrace> _recipients;

        public ICollection<IncomingTrace> Recipients
        {
            get { return _recipients ?? (_recipients = new List<IncomingTrace>()); }
            set { _recipients = value; }
        }
    }
}
