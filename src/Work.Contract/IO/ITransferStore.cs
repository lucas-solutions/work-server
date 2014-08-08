using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Persistence;

    public interface ITransferStore : IEntityStore<Transfer>
    {
        /// <summary>
        /// Find a transfer by id
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<Party> FindByIdAsync(int transferId);

        /// <summary>
        /// Find a transfer by name
        /// </summary>
        /// <param name="transferName"></param>
        /// <returns></returns>
        Task<Party> FindByNameAsync(string transferName);
    }
}
