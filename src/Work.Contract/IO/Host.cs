using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class Host
    {
        public static implicit operator string(Host host)
        {
            return host != null ? host.ToString() : null;
        }

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

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Port
        {
            get;
            set;
        }

        public HostProtocol Protocol
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name ?? "[Host]";
        }
    }
}
