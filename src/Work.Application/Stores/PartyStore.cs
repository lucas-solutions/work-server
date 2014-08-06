using System;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;
    
    public class PartyStore : EntityStore<Party>, IPartyStore
    {
        public Task<Party> FindByIdAsync(int partyId)
        {
            throw new NotImplementedException();
        }

        public Task<Party> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}