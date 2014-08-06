using System.Collections.Generic;
using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    [ConfigurationCollection(typeof(HostElement), AddItemName = "host")]
    public class HostElementCollection : ConfigurationElementCollection, IEnumerable<HostElement>
    {
        private readonly List<HostElement> _hosts;

        public HostElementCollection()
        {
            _hosts = new List<HostElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var host = new HostElement();

            _hosts.Add(host);

            return host;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var host = element as HostElement;
            return host != null ? host.Name : new System.Random().Next().ToString();
        }

        public new IEnumerator<HostElement> GetEnumerator()
        {
            return _hosts.GetEnumerator();
        }
    }
}
