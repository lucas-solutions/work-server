using System;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;

    public class OutgoingTraceDocumentStore : EntityDocumentStore<OutgoingTrace>, IOutgoingTraceStore
    {
        public Task<IncomingTrace> FindByIdAsync(int traceId)
        {
            throw new NotImplementedException();
        }
    }
}
