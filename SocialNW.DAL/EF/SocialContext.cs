using Microsoft.AspNet.Identity.EntityFramework;
using SocialNW.DAL.Entities;
using System.Data.Entity;

namespace SocialNW.DAL.EF
{
    public class SocialContext : IdentityDbContext<AppUser, CustomRole,
           int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        static SocialContext()
        {
            Database.SetInitializer<SocialContext>(new DbInitializer());
        }

        public SocialContext(string conectionString) : base(conectionString) { }

        public SocialContext() : base("name=SocialNW_DB") { }



        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FriendRequest> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static SocialContext Create()
        {
            return new SocialContext();
        }
    }
}
