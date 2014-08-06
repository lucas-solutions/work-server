using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Models
{
    public class UserRolesViewModel
    {
        public string Department { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public int IncomingCount { get; set; }
        
        public int OutgoingCount { get; set; }

        public int TranferCount { get; set; }

        public Dictionary<string, bool> Roles { get; set; }
    }
}