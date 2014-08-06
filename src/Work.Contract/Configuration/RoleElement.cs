using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.Authorization;

    public class RoleElement : ConfigurationElement
    {
        public static implicit operator Role(RoleElement el)
        {
            return el == null ? null : new Role
            {
                Name = el.Name
            };
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
    }
}
