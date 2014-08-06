using System.Collections.Generic;
using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    [ConfigurationCollection(typeof(PartyElement), AddItemName = "party")]
    public class PartyElementCollection : ConfigurationElementCollection, IEnumerable<PartyElement>
    {
        private readonly List<PartyElement> _parties;

        public PartyElementCollection()
        {
            _parties = new List<PartyElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var party = new PartyElement();

            _parties.Add(party);

            return party;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var party = element as PartyElement;
            return party != null ? party.Name : string.Empty;
        }

        public new IEnumerator<PartyElement> GetEnumerator()
        {
            return _parties.GetEnumerator();
        }
    }
}
