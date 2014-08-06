using System.Collections.Generic;
using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    [ConfigurationCollection(typeof(TransferElement), AddItemName = "transfer")]
    public class TransferElementCollection : ConfigurationElementCollection, IEnumerable<TransferElement>
    {
        private readonly List<TransferElement> _transfers;

        public TransferElementCollection()
        {
            _transfers = new List<TransferElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var task = new TransferElement();

            _transfers.Add(task);

            return task;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var transfer = element as TransferElement;
            return transfer != null ? transfer.Name : string.Empty;
        }

        public new IEnumerator<TransferElement> GetEnumerator()
        {
            return _transfers.GetEnumerator();
        }
    }
}
