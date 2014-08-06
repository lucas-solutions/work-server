using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Authentication
{
    using Lucas.Solutions.Authorization;

    public class User
    {
        private ICollection<Role> _roles;

        public string Email
        {
            get;
            set;
        }

        public ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
    }
}
