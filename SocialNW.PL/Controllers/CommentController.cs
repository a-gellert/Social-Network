using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult GetComments(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            return View();
        }
    }
}