using System;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;
    
    public class OutgoingTraceStore : EntityStore<OutgoingTrace>, IOutgoingTraceStore
    {
        public Task<IncomingTrace> FindByIdAsync(int traceId)
        {
            throw new NotImplementedException();
        }
    }
}