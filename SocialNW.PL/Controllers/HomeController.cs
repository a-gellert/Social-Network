using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers

{   [Authorize]
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
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));
            var users = mapper.Map<IEnumerable<UProfileDTO>, IEnumerable<ProfileViewModel>>(_profileService.GetAll());

            return View(users);
        }

        public ActionResult GetUserFriends()
        {
            var friends = _profileService.GetFriends(User.Identity.GetUserId<int>());

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));
            var userFriends = mapper.Map<IEnumerable<UProfileDTO>, IEnumerable<ProfileViewModel>>(friends);

            return View(userFriends);
        }

        public ActionResult GetUserById(int? id)
        {
            var userId = id ?? User.Identity.GetUserId<int>();


            var user = _profileService.GetById(userId);

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));
            var currentUser = mapper.Map< UProfileDTO, ProfileViewModel> (user);

            return View(currentUser);
        }

        public ActionResult Search(string searchString, string country, string city)
        {
            IEnumerable<UProfileDTO> usersDto;

                usersDto = _profileService.Search(searchString, country, city);


            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UProfileDTO, ProfileViewModel>()));
            var users = mapper.Map<IEnumerable<UProfileDTO>, IEnumerable<ProfileViewModel>>(usersDto);

            return View("GetAllUsers", users);
        }
    }
}