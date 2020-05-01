using Microsoft.AspNet.Identity.EntityFramework;
using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private SocialContext _db;

        public PostRepository(SocialContext context)
        {
            _db = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return _db.Posts;
        }

        public Post GetById(int id)
        {
            return _db.Posts.Find(id);
        }

        public void Create(Post post)
        {
            _db.Posts.Add(post);
        }

        public void Update(Post post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }

        public IEnumerable<Post> Find(Func<Post, Boolean> predicate)
        {
            return _db.Posts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Post post = _db.Posts.Find(id);
            if (post != null)
                _db.Posts.Remove(post);
        }


    }
}
