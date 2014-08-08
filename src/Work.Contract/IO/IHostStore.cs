using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Persistence;

    public interface IHostStore : IEntityStore<Host>, IQueryableEntityStore<Host>
    {
        Task<IQueryable<Host>> Hosts { get; }

        /// <summary>
        /// Find a host by id
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        Task<Host> FindByIdAsync(int hostId);

        /// <summary>
        /// Find a host by name
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<Host> FindByNameAsync(string hostName);
    }
}
