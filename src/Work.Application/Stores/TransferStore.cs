using System;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;
    
    public class TransferStore : EntityStore<Transfer>, ITransferStore
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