using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.IO;

    public class TransfersController : ApiController
    {
        // GET: api/Transfers
        public IEnumerable<Transfer> Get()
        {
            return new Transfer[] { new Transfer(), new Transfer() };
        }

        // GET: api/Transfers/5
        public Transfer Get(int id)
        {
            return new Transfer();
        }

        // POST: api/Transfers
        public void Post([FromBody]Transfer value)
        {
        }

        // PUT: api/Transfers/5
        public void Put(int id, [FromBody]Transfer value)
        {
        }

        // DELETE: api/Transfers/5
        public void Delete(int id)
        {
        }
    }
}
