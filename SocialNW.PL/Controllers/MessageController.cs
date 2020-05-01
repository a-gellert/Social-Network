using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MessageViewModel model)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(MessageViewModel model)
        {
            return View();
        }

        public ActionResult HasUnreadedMessages(int id)
        {
            return View();
        }

        public ActionResult GetUserMessages(int id)
        {
            return View();
        }
    }
}