using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Persistence;

    public interface IOutgoingTraceStore : IEntityStore<OutgoingTrace>, IQueryableEntityStore<OutgoingTrace>
    {
        /// <summary>
        /// Find an outgoingg trace by id
        /// </summary>
        /// <param name="traceId"></param>
        /// <returns></returns>
        Task<IncomingTrace> FindByIdAsync(int traceId);
    }
}
