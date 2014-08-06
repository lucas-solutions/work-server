using System.Collections.Generic;
using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    [ConfigurationCollection(typeof(RoleElement), AddItemName = "role")]
    public class RoleElementCollection : ConfigurationElementCollection, IEnumerable<RoleElement>
    {
        private readonly List<RoleElement> _roles;

        public RoleElementCollection()
        {
            _roles = new List<RoleElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var role = new RoleElement();

            _roles.Add(role);

            return role;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var role = element as RoleElement;
            return role != null ? role.Name : string.Empty;
        }

        public new IEnumerator<RoleElement> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }
    }
}
