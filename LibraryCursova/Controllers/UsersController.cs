using LibraryCursova.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryCursova.Controllers
{
    public class UsersController : Controller
    {
        BookContext db = new BookContext();
        private ApplicationUserManager _userManager;

        public UsersController()
        {

        }

        public UsersController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            var users = db.Users.ToList().Select(c => {

                var role = c.Roles.FirstOrDefault();

                return new ManagedUsers
                {
                    RoleId = role == null ? "" : role.RoleId,
                    RoleName = role == null ? "" : roles.First(b => b.Id == c.Roles.First().RoleId).Name,
                    UserId = c.Id,
                    UserName = c.UserName
                };
            }).ToList();
            ViewBag.Roles = roles;
            return View(users);
        }

        [HttpGet]
        public async Task EditUser(string userId, string roleId, string oldRoleName)
        {
            var role = db.Roles.First(c => c.Id == roleId);
            if (!string.IsNullOrEmpty(oldRoleName))
            {
                var res = await UserManager.RemoveFromRoleAsync(userId, oldRoleName);
            }
            var res1 = await UserManager.AddToRoleAsync(userId, role.Name);
            await db.SaveChangesAsync();
        }
    }
}