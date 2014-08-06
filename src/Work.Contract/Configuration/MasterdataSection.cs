using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    public class MasterdataSection : ConfigurationSection 
    {
        [ConfigurationProperty("application", IsRequired = true)]
        public string Application
        {
            get { return (string)base["application"]; }
            set { base["application"] = value; }
        }

        [ConfigurationProperty("hosts", IsRequired = false)]
        public HostElementCollection Hosts
        {
            get { return (HostElementCollection)base["hosts"]; }
            set { base["hosts"] = value; }
        }

        [ConfigurationProperty("roles", IsRequired = false)]
        public RoleElementCollection Roles
        {
            get { return (RoleElementCollection)base["roles"]; }
            set { base["roles"] = value; }
        }

        [ConfigurationProperty("transfers", IsRequired = false)]
        public TransferElementCollection Transfers
        {
            get { return (TransferElementCollection)base["transfers"]; }
            set { base["transfers"] = value; }
        }

        [ConfigurationProperty("users", IsRequired = false)]
        public UserElementCollection Users
        {
            get { return (UserElementCollection)base["users"]; }
            set { base["users"] = value; }
        }
    }
}
