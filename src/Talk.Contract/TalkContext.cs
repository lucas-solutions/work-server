using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public class TalkContext
    {
        public string Address
        {
            get;
            set;
        }

        public string Credential
        {
            get;
            set;
        }

        public TalkFormat Format
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public int? Port
        {
            get;
            set;
        }

        public TalkProtocol Protocol
        {
            get;
            set;
        }
    }
}
