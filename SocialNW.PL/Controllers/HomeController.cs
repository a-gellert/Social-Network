using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNW.BLL.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers

{   [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        private IProfileService _profileService;

        public HomeController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("GetUserById");
        }

        public ActionResult GetAllUsers()
        {
            var users = _profileService.GetAll();

            return View(users);
        }

        public ActionResult GetUserFriends()
        {
            var friends = _profileService.GetFriends(User.Identity.GetUserId<int>());

            return View(friends);
        }

        public ActionResult GetUserById(int? id)
        {
            var userId = id ?? User.Identity.GetUserId<int>();
            var currentUser = _profileService.GetById(userId);
            return View(currentUser);
        }

        public ActionResult Search(string searchString, string country, string city)
        {
            var users = _profileService.Search(searchString, country, city);
            
            return View("GetAllUsers", users);
        }
    }
}