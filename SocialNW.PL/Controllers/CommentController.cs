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
{   [Authorize]
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public ActionResult GetComments(int? id)
        {
            var comments = _commentService.GetCommentsToPost(id);

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, CommentViewModel>()));
            var commentViews = mapper.Map<IEnumerable<CommentDTO>, List<CommentViewModel>>(comments);

            return View(commentViews);
        }
        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            try
            {
                CommentDTO commentDto = new CommentDTO()
                {
                    Text = model.Text,
                    PostId = model.PostId,
                    CommentDate = DateTime.Now,
                    AppUserId = User.Identity.GetUserId<int>()
                };
                if (string.IsNullOrEmpty(commentDto.Text))
                {
                    ModelState.AddModelError("Text", "Comment text can't be empty");
                }
                _commentService.Create(commentDto);
            }
            catch (ValidationException)
            {
                ModelState.AddModelError("Text", "Can't create comment");
            }
            return RedirectToAction("GetComments");
        }
    }
}