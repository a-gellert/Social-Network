using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult GetPostsByUser(int id)
        {
            var postsDto = _postService.GetPostsByUser(id);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, PostViewModel>()));
            var postsViewModel = mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostViewModel>>(postsDto);

            return PartialView(postsViewModel);
        }
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                PostDTO postDto = new PostDTO()
                {
                    Text = formCollection["Post"],
                    Name = formCollection["name"],
                    AppUserId = User.Identity.GetUserId<int>()
                };
                _postService.Create(postDto);
                
                return RedirectToAction("GetUserById","Home");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        public ActionResult GetFriendsPosts(int id)
        {
            try
            {
                var postsDto = _postService.GetFriendsPosts(id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, PostViewModel>()));
                var postsViewModel = mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostViewModel>>(postsDto);
                return PartialView("GetPostsByUser", postsViewModel);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}