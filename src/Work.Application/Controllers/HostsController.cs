using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.IO;
    using Lucas.Solutions.Stores;

    public class HostsController : ApiController
    {
        private IHostStore _hostStore;

        public IHostStore HostStore
        {
            get { return _hostStore ?? (_hostStore = new HostStore()); }
            set { _hostStore = value; }
        }

        // GET: api/Hosts
        public IEnumerable<Host> Get()
        {
            return new Host[] { new Host(), new Host() };
        }

        // GET: api/Hosts/5
        public Host Get(int id)
        {
            return new Host();
        }

        // POST: api/Hosts
        public void Post([FromBody]Host value)
        {
        }

        // PUT: api/Hosts/5
        public void Put(int id, [FromBody]Host value)
        {
        }

        // DELETE: api/Hosts/5
        public void Delete(int id)
        {
        }
    }
}
