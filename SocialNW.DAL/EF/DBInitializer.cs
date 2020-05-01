using Microsoft.AspNet.Identity;
using SocialNW.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<SocialContext>
    {
        protected override void Seed(SocialContext context)
        {
            var userManager = new UserManager<AppUser, int>(new CustomUserStore(context));

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));

            var role1 = new CustomRole { Name = "admin" };
            var role2 = new CustomRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new AppUser
            {
                Email = "gellert838@gmail.com",
                UserName = "gellert838@gmail.com",
                Profile = new UserProfile { FirstName = "Alexander", LastName = "Gellert" }
            };

            string password = "123456wE!";
            var result = userManager.CreateAsync(admin, password);

            if (result.IsCompleted)
            {
                userManager.AddToRoleAsync(admin.Id, role1.Name);
                userManager.AddToRoleAsync(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}
