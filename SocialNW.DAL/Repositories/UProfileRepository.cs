using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNW.DAL.Repositories
{
    public class UProfileRepository : IRepository<UserProfile>
    {
        private SocialContext _db;

        public UProfileRepository(SocialContext context)
        {
            _db = context;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _db.UserProfiles;
        }

        public UserProfile GetById(int id)
        {
            return _db.UserProfiles.Find(id);
        }

        public void Create(UserProfile profile)
        {
            _db.UserProfiles.Add(profile);
        }

        public void Update(UserProfile profile)
        {
            _db.Entry(profile).State = EntityState.Modified;
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, Boolean> predicate)
        {
            return _db.UserProfiles.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            UserProfile profile = _db.UserProfiles.Find(id);
            if (profile != null)
                _db.UserProfiles.Remove(profile);
        }


    }
}
