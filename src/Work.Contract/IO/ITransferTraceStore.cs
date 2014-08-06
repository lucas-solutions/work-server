using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public interface ITransferTraceStore : IEntityStore<TransferTrace>, IQueryableEntityStore<TransferTrace>
    {
        /// <summary>
        /// Find an incomming trace by id
        /// </summary>
        /// <param name="traceId"></param>
        /// <returns></returns>
        Task<IncomingTrace> FindByIdAsync(int traceId);
    }
}
