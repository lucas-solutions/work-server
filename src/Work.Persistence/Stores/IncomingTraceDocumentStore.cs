using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;

    public class IncomingTraceDocumentStore : EntityDocumentStore<IncomingTrace>, IIncomingTraceStore
    {
        public Task<IncomingTrace> FindByIdAsync(int traceId)
        {
            throw new NotImplementedException();
        }
    }
}
