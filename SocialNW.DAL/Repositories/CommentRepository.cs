using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNW.DAL.Repositories
{
   public class CommentRepository : IRepository<Comment>
    {
        private SocialContext _db;

        public CommentRepository(SocialContext context)
        {
            _db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments;
        }

        public Comment GetById(int id)
        {
            return _db.Comments.Find(id);
        }

        public void Create(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
        }

        public IEnumerable<Comment> Find(Func<Comment, Boolean> predicate)
        {
            return _db.Comments.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Comment comment = _db.Comments.Find(id);
            if (comment != null)
                _db.Comments.Remove(comment);
        }


    }
}
