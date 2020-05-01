using Microsoft.AspNet.Identity;
using SocialNW.DAL.Entities;
using System;

namespace SocialNW.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Message> Messages { get; }
        IRepository<Post> Posts { get; }
        IRepository<UserProfile> Profiles { get; }
        IRepository<Comment> Comments { get; }
        IRepository<FriendRequest> Requests { get; }
        UserManager<AppUser, int> UserManager { get; }

        void Save();
    }
}
