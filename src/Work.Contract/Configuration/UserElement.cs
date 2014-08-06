using System;
using System.Configuration;
using System.Linq;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.Authentication;
    using Lucas.Solutions.Authorization;

    public class UserElement : ConfigurationElement
    {
        public static implicit operator User(UserElement el)
        {
            return el == null ? null : new User
            {
                Email = el.Email,
                Roles = Array.AsReadOnly(el.Roles.Split(new [] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(name => new Role { Name = name }).ToArray())
            };
        }

        [ConfigurationProperty("email", IsRequired = true)]
        public string Email
        {
            get { return (string)base["email"]; }
            set { base["email"] = value; }
        }

        [ConfigurationProperty("roles", IsRequired = false)]
        public string Roles
        {
            get { return (string)base["roles"]; }
            set { base["roles"] = value; }
        }
    }
}
