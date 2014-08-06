using System;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;
    
    public class IncomingTraceStore : EntityStore<IncomingTrace>, IIncomingTraceStore
    {
        public Task<IncomingTrace> FindByIdAsync(int traceId)
        {
            throw new NotImplementedException();
        }
    }
}