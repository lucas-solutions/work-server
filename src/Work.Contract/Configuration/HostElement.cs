using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.IO;

    public class HostElement : ConfigurationElement
    {
        public static implicit operator Host(HostElement el)
        {
            return el == null ? null : new Host
            {
                Address = el.Address,
                Credential = el.Credential,
                Name = el.Name,
                Password = el.Password,
                Port = el.Port,
                //Protocol = el.Protocol,
                Summary = el.Summary
            };
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)base["address"]; }
            set { base["address"] = value; }
        }

        [ConfigurationProperty("credential", IsRequired = false)]
        public string Credential
        {
            get { return (string)base["credential"]; }
            set { base["credential"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = false)]
        public string Port
        {
            get { return (string)base["port"]; }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("protocol", IsRequired = true)]
        public string Protocol
        {
            get { return (string)base["protocol"]; }
            set { base["protocol"] = value; }
        }

        [ConfigurationProperty("summary", IsRequired = false)]
        public string Summary
        {
            get { return (string)base["summary"]; }
            set { base["summary"] = value; }
        }
    }
}
