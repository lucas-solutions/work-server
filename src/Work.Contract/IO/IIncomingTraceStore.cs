using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Persistence;

    public interface IIncomingTraceStore : IEntityStore<IncomingTrace>, IQueryableEntityStore<IncomingTrace>
    {
        /// <summary>
        /// Find an incomming trace by id
        /// </summary>
        /// <param name="traceId"></param>
        /// <returns></returns>
        Task<IncomingTrace> FindByIdAsync(int traceId);
    }
}
