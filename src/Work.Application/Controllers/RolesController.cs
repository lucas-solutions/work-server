using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.Stores;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RolesController : ApiController
    {
        public static string ToId(string id) { return id.Trim().ToLower().Replace(' ', '-'); }

        private ApplicationDbContext _dbContext;
        private RoleStore<IdentityRole> _roleStore;

        public ApplicationDbContext DbContext
        {
            get {  return _dbContext ?? (_dbContext = ApplicationDbContext.Create()); }
        }

        public RoleStore<IdentityRole> RoleStore
        {
            get { return _roleStore ?? (_roleStore = new RoleStore<IdentityRole>(DbContext)); }
        }

        // GET: api/Roles
        public IEnumerable<string> Get()
        {
            return RoleStore.Roles.Select(r => r.Name);
        }

        // GET: api/Roles/5
        public async Task<string> Get(string id)
        {
            var task = await RoleStore.FindByIdAsync(id.ToLower());
            return task != null ? task.Name : null;
        }

        // POST: api/Roles
        public async void Post([FromBody]string value)
        {
            await RoleStore.CreateAsync(new IdentityRole { Id = ToId(value), Name = value });
        }

        // PUT: api/Roles/5
        public async void Put(string id, [FromBody]string value)
        {
            var role = await RoleStore.FindByIdAsync(id.ToLower());

            if (role != null)
            {
                await RoleStore.UpdateAsync(new IdentityRole { Id = ToId(id), Name = value });
            }
        }

        // DELETE: api/Roles/5
        public async void Delete(string id)
        {
            var role = await RoleStore.FindByIdAsync(ToId(id));

            if (role != null)
            {
                await RoleStore.DeleteAsync(role);
            }
        }
    }
}