using Microsoft.AspNet.Identity;
using SocialNW.DAL.EF;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SocialContext _db;

        private bool _isDisposed = false;

        private UserManager<AppUser, int> _manager;
        private PostRepository _postRepository;
        private CommentRepository _commentRepository;
        private FriendRequestRepository _requestRepository;
        private UProfileRepository _profileRepository;
        private MessageRepository _messageRepository;

        public UnitOfWork(string connectionString)
        {
            _db = new SocialContext(connectionString);
        }

        public IRepository<Message> Messages
        {
            get
            {
                return _messageRepository ?? (_messageRepository = new MessageRepository(_db));
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return _postRepository ?? (_postRepository = new PostRepository(_db));
            }
        }

        public IRepository<UserProfile> Profiles
        {
            get
            {
                return _profileRepository ?? (_profileRepository = new UProfileRepository(_db));
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return _commentRepository ?? (_commentRepository = new CommentRepository(_db));
            }
        }

        public IRepository<FriendRequest> Requests
        {
            get
            {
                return _requestRepository ?? (_requestRepository = new FriendRequestRepository(_db));
            }
        }

        public UserManager<AppUser, int> UserManager
        {
            get
            {
                return _manager ?? (_manager = new UserManager<AppUser, int>(new CustomUserStore(_db)));
            }
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
