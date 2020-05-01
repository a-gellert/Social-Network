using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNW.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.Entities
{
    public class AppUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public AppUser()
        {
            Posts = new List<Post>();
            Messages = new List<Message>();
            Friends = new List<UserProfile>();
            Comments = new List<Comment>();
            Requests = new List<FriendRequest>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserProfile> Friends { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FriendRequest> Requests { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<AppUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(SocialContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(SocialContext context)
            : base(context)
        {
        }
    }
}
