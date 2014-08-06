using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;

    public class TransferDocumentStore : EntityDocumentStore<Transfer>, ITransferStore
    {
        public Task<Party> FindByIdAsync(int transferId)
        {
            throw new NotImplementedException();
        }

        public Task<Party> FindByNameAsync(string transferName)
        {
            throw new NotImplementedException();
        }
    }
}
