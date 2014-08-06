using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.Models;
    using Lucas.Solutions.Stores;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System.Net.Http;

    public class WorkController : Controller
    {
        private ApplicationDbContext _dbContext;
        private RoleStore<IdentityRole> _roleStore;
        private ApplicationUserManager _userManager;

        public WorkController()
        {
        }

        public ApplicationDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = ApplicationDbContext.Create()); }
        }

        public RoleStore<IdentityRole> RoleStore
        {
            get { return _roleStore ?? (_roleStore = new RoleStore<IdentityRole>(DbContext)); }
        }

        public WorkController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Work Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users(bool? detail, int? start, int? limit)
        {
            IQueryable<ApplicationUser> users = UserManager.Users;
            var total = UserManager.Users.Count();

            if (start.HasValue)
            {
                users.Skip(start.Value);
            }

            var roles = RoleStore.Roles.ToDictionary(r => r.Id, r => false);

            object results = users.ToList().Select(user =>
                new UserRolesViewModel
                {
                    Department = user.Department,
                    Email = user.Email,
                    FullName = user.FullName,
                    Roles = user.Roles.ToDictionary(userRole => userRole.RoleId, check => true),
                    IncomingCount = 0,
                    OutgoingCount = 0,
                    TranferCount = 0
                }).ToList();

            return Json(new { success = true, total = total, request = new { start = start, limit = limit }, results = results }, JsonRequestBehavior.AllowGet);
        }
    }
}