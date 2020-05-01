using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult GetProfile(int id)
        {
            return View();
        }
        
        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProfileViewModel model)
        {
            return View();
        }

        public FileContentResult UserPhotos(int? id)
        {
            byte[] imageData = null;

            return File(imageData, null);
        }
    }
}