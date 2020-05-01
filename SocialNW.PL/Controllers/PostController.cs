using AutoMapper;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
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

        public ActionResult Create(PostViewModel model)
        {
            try
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostViewModel, PostDTO>()));
                var postModel = mapper.Map<PostViewModel, PostDTO>(model);

                _postService.Create(postModel);
                
                return RedirectToAction("GetUserById", "Home");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        public ActionResult GetPostByFriend(int id)
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