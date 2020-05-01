using AutoMapper;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Interfaces;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNW.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>());
            var mapper = new Mapper(config);
            return  mapper.Map<List<CommentDTO>>(_unitOfWork.Comments.GetAll()); ;
        }

        public void Create(CommentDTO commentDto)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());

                var mapper = new Mapper(config);
                var comment = mapper.Map<CommentDTO, Comment>(commentDto);
                if (comment != null)
                {
                    _unitOfWork.Comments.Create(comment);
                }
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CommentDTO> GetCommentsToPost(int id)
        {
            var comments = _unitOfWork.Comments.GetAll().Where(x => x.PostId == id).OrderByDescending(x => x.CommentDate).ToList();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());
            var mapper = new Mapper(config);

            return mapper.Map<List<Comment>, List<CommentDTO>>(comments);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
