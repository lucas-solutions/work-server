using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public abstract class Trace : ITrace
    {
        private readonly string _type;

        protected Trace()
        {
            Type = _type = GetType().Name;
        }

        public virtual TimeSpan Duration
        {
            get;
            set;
        }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual string Message
        {
            get;
            set;
        }

        public virtual DateTimeOffset Start
        {
            get;
            set;
        }

        public virtual bool Success
        {
            get;
            set;
        }

        public virtual string Type
        {
            get;
            set;
        }
    }
}
