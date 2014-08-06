using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.IO;

    public class HostDocumentStore : EntityDocumentStore<Host>, IHostStore
    {
        public Task<IQueryable<Host>> Hosts
        {
            get
            {
                return null;
            }
        }

        public Task<Host> FindByIdAsync(int hostId)
        {
            throw new NotImplementedException();
        }

        public Task<Host> FindByNameAsync(string hostName)
        {
            throw new NotImplementedException();
        }
    }
}
