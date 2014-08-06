using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucas.Solutions.Controllers
{
    using Lucas.Solutions.Models;
    using Microsoft.AspNet.Identity.Owin;

    /// <summary>
    /// My Status Controller
    /// </summary>
    [Authorize]
    public class StatusController : Controller
    {
        private ApplicationUserManager _userManager;

        public StatusController()
        {
        }

        public StatusController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Status
        public ActionResult Index()
        {
            return View();
        }
    }
}