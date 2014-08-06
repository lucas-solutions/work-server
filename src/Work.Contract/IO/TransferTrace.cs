using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Automation;

    public abstract class TransferTrace : Trace
    {
        public virtual TransferDirection Direction
        {
            get;
            set;
        }

        public virtual string File
        {
            get;
            set;
        }

        public virtual Party Party
        {
            get;
            set;
        }

        public virtual int PartyId
        {
            get;
            set;
        }

        public virtual long Size
        {
            get;
            set;
        }

        public virtual Transfer Transfer
        {
            get;
            set;
        }

        public virtual int TransferId
        {
            get;
            set;
        }
    }
}
