using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public abstract class TransferDirectory
    {
        protected TransferDirectory(Party party)
        {
            Party = party;
        }

        public Party Party
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }
    }
}
