using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.IO;

    public class PartiesController : ApiController
    {
        private IPartyStore _partyStore;

        // GET: api/Parties
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Parties/5
        public Party Get(int id)
        {
            return new Party();
        }

        // POST: api/Parties
        public void Post([FromBody]Party value)
        {
        }

        // PUT: api/Parties/5
        public void Put(int id, [FromBody]Party value)
        {
        }

        // DELETE: api/Parties/5
        public void Delete(int id)
        {
        }
    }
}
