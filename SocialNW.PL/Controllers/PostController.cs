using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult GetPostsByUser(int id)
        {
            return View();
        }

        public ActionResult Create(PostViewModel model)
        {
            return View();
        }


        public ActionResult GetPostByFriend(int id)
        {
            return View();
        }
    }
}