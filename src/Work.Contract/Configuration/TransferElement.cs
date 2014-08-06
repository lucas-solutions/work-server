using System;
using System.Configuration;
using System.Linq;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.IO;

    public class TransferElement : TaskElement
    {
        public static implicit operator Transfer(TransferElement el)
        {
            return el == null ? null : new Transfer
            {
                Name = el.Name,
                Parties = Array.AsReadOnly(el.Parties.Select(pel => (Party)pel).ToArray()),
                Start = el.Start,
                Summary = el.Summary
            };
        }

        [ConfigurationProperty("parties", IsRequired = true)]
        public PartyElementCollection Parties
        {
            get { return (PartyElementCollection)base["parties"]; }
            set { base["parties"] = value; }
        }
    }
}
