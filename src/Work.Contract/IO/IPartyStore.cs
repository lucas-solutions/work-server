using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public interface IPartyStore : IEntityStore<Party>, IQueryableEntityStore<Party>
    {
        /// <summary>
        /// Find a party by id
        /// </summary>
        /// <param name="partyId"></param>
        /// <returns></returns>
        Task<Party> FindByIdAsync(int partyId);

        /// <summary>
        /// Find a party by name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<Party> FindByNameAsync(string roleName);
    }
}
