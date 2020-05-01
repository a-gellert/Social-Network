using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    public class ProfileController : Controller
    {
        private  IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ActionResult GetProfile(int id)
        {
            try
            {
                var profileDto = _profileService.GetById(id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));

                var profile = mapper.Map<UProfileDTO, ProfileViewModel>(profileDto);
                return PartialView(profile);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        
        public ActionResult Edit(int id = 0)
        {
            try
            {
                UProfileDTO profileDto = _profileService.GetById(id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));

                var profile = mapper.Map<UProfileDTO, ProfileViewModel>(profileDto);

                return View(profile);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "UserPhoto")]ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProfileViewModel, UProfileDTO>()));
                var profileDto = mapper.Map<ProfileViewModel, UProfileDTO>(model);

                byte[] imageData = null;

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];
                    if (poImgFile != null && poImgFile.ContentLength > 0)
                    {
                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            imageData = binary.ReadBytes(poImgFile.ContentLength);
                        }
                    }
                }

                if (imageData != null)
                {
                    profileDto.UserPhoto = imageData;
                }
                try
                {
                    _profileService.Update(profileDto);
                    return RedirectToAction("GetUserById", "Home");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return View(model);
        }

        public FileContentResult UserPhotos(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = id ?? User.Identity.GetUserId<int>();

                var userProfile = _profileService.GetById(userId);

                return new FileContentResult(userProfile.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        imageData = br.ReadBytes((int)imageFileLength);
                    }
                }
                return File(imageData, "image/png");
            }
        }
    }
}