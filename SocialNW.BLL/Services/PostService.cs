using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNW.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PostDTO postDto)
        {
            try
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, Post>()));
                var post = mapper.Map<PostDTO, Post>(postDto);

                post.ApplicationUser = _unitOfWork.UserManager.FindById(post.AppUserId ?? 0);
                _unitOfWork.Posts.Create(post);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Posts.Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<PostDTO> GetAll()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()));

            return mapper.Map<List<Post>, List<PostDTO>>(_unitOfWork.Posts.GetAll().ToList());
        }

        public PostDTO GetById(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                throw new ValidationException("Post doesn't exist", "");

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()));

            return mapper.Map<Post, PostDTO>(post);
        }

        public IEnumerable<PostDTO> GetFriendsPosts(int id)
        {
            var user = _unitOfWork.UserManager.FindById(id);
            if (user == null)
                throw new ValidationException("User doesn't exist", "");
            var friendIds = user.Friends.Select(p => p.Id);
            var posts = _unitOfWork.Posts.GetAll().Where(x => friendIds.Contains((int)x.AppUserId)).ToList();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()));

            return mapper.Map<List<Post>, List<PostDTO>>(posts);
        }

        public IEnumerable<PostDTO> GetPostsByUser(int id)
        {
            var posts = _unitOfWork.Posts.GetAll().Where(x => x.AppUserId == id).OrderByDescending(x => x.Date).ToList();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()));
            
            return mapper.Map<List<Post>, List<PostDTO>>(posts);
        }

        public void Update(int id, PostDTO postDto)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                throw new ValidationException("Post doesn't exist", "");

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, Post>()));
            var newPost = mapper.Map<PostDTO, Post>(postDto);

            _unitOfWork.Posts.Update(newPost);
            _unitOfWork.Save();
        }
    }
}
