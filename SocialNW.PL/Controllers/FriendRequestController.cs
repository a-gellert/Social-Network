using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    [Authorize]
    public class FriendRequestController : Controller
    {
        private readonly IFriendService _friendService;

        public FriendRequestController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        public ActionResult GetRequestsById(int id)
        {
            var requestsDto = _friendService.GetRequestsById(id);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<FriendRequestDTO, FriendRequestViewModel>()));
            var friendRequests = mapper.Map<IEnumerable<FriendRequestDTO>, List<FriendRequestViewModel>>(requestsDto);
            
            return View(friendRequests);
        }

        public ActionResult Create(int friendUserId)
        {
            try
            {
                _friendService.Create(User.Identity.GetUserId<int>(), friendUserId);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Reject(int id)
        {
            try
            {
                _friendService.Reject(id);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Accept(int id)
        {
            try
            {
                _friendService.Accept(id);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var count = _friendService.HasWaitingRequest(id);

            return Json(new { countRequests = count },
                 JsonRequestBehavior.AllowGet);
        }
    }
}