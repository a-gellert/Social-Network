using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class FriendRequestController : Controller
    {
        public ActionResult GetRequestsById(int id)
        {
            return View();
        }

        public ActionResult Create(int friendUserId)
        {
            return View();
        }

        public ActionResult Reject(int id)
        {

            return View();
        }

        public ActionResult Accept(int id)
        {
            return View();
        }

        public ActionResult HasWaitingRequest(int id)
        {

            return View();
        }
    }
}