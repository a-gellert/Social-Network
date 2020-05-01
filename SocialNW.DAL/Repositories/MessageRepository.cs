using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNW.DAL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private SocialContext _db;

        public MessageRepository(SocialContext context)
        {
            _db = context;
        }

        public IEnumerable<Message> GetAll()
        {
            return _db.Messages;
        }

        public Message GetById(int id)
        {
            return _db.Messages.Find(id);
        }

        public void Create(Message message)
        {
            _db.Messages.Add(message);
        }

        public void Update(Message message)
        {
            _db.Entry(message).State = EntityState.Modified;
        }

        public IEnumerable<Message> Find(Func<Message, Boolean> predicate)
        {
            return _db.Messages.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Message message = _db.Messages.Find(id);
            if (message != null)
                _db.Messages.Remove(message);
        }
    }
}
