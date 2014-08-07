using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.IO;

    public class PartyElement : ConfigurationElement
    {
        public static implicit operator Party(PartyElement el)
        {
            return el == null ? null : new Party
            {
                Direction = el.Direction,
                Email = el.Email,
                Host = new Host { Name = el.Host },
                Name = el.Name,
                Password = el.Password,
                Path = el.Path,
                Summary = el.Summary,
                User = el.User,
            };
        }

        [ConfigurationProperty("direction", IsRequired = true)]
        public virtual TransferDirection Direction
        {
            get { return (TransferDirection)base["direction"]; }
            set { base["direction"] = value; }
        }

        [ConfigurationProperty("email", IsRequired = true)]
        public virtual string Email
        {
            get { return (string)base["email"]; }
            set { base["email"] = value; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public virtual string Host
        {
            get { return (string)base["host"]; }
            set { base["host"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public virtual string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public virtual string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true)]
        public virtual string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("summary", IsRequired = false)]
        public virtual string Summary
        {
            get { return (string)base["summary"]; }
            set { base["summary"] = value; }
        }

        [ConfigurationProperty("user", IsRequired = false)]
        public virtual string User
        {
            get { return (string)base["user"]; }
            set { base["user"] = value; }
        }
    }
}
