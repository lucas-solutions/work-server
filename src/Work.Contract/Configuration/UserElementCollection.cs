using System.Collections.Generic;
using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    [ConfigurationCollection(typeof(UserElement), AddItemName = "user")]
    public class UserElementCollection : ConfigurationElementCollection, IEnumerable<UserElement>
    {
        private readonly List<UserElement> _users;

        public UserElementCollection()
        {
            _users = new List<UserElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var user = new UserElement();

            _users.Add(user);

            return user;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var user = element as UserElement;
            return user != null ? user.Email : string.Empty;
        }

        public new IEnumerator<UserElement> GetEnumerator()
        {
            return _users.GetEnumerator();
        }
    }
}
