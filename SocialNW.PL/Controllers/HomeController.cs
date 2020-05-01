using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUsers()
        {
            return View();
        }

        public ActionResult GetUserFriends()
        {
            return View();
        }

        public ActionResult GetUserById()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserById(int? id)
        {
            return View();
        }
    }
}