using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Automation;

    public class Transfer : Task
    {
        private ICollection<Party> _parties;

        public virtual ICollection<Party> Parties
        {
            get { return _parties ?? (_parties = new List<Party>()); }
            set { _parties = value; }
        }
    }
}
