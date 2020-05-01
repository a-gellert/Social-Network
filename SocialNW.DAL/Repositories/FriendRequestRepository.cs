using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNW.DAL.Repositories
{
    public class FriendRequestRepository : IRepository<FriendRequest>
    {
        private SocialContext _db;

        public FriendRequestRepository(SocialContext context)
        {
            _db = context;
        }

        public IEnumerable<FriendRequest> GetAll()
        {
            return _db.Friends;
        }

        public FriendRequest GetById(int id)
        {
            return _db.Friends.Find(id);
        }

        public void Create(FriendRequest request)
        {
            _db.Friends.Add(request);
        }

        public void Update(FriendRequest request)
        {
            _db.Entry(request).State = EntityState.Modified;
        }

        public IEnumerable<FriendRequest> Find(Func<FriendRequest, Boolean> predicate)
        {
            return _db.Friends.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            FriendRequest request = _db.Friends.Find(id);
            if (request != null)
                _db.Friends.Remove(request);
        }


    }
}
