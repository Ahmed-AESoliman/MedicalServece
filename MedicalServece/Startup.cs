using MedicalServece.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MedicalServece.Startup))]
namespace MedicalServece
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUser();
        }
        public void CreateDefaultRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.FullUserName = "Administrator";
                user.UserName = "ahmed@gm.co";
                user.Email = "ahmed@gm.co";
                user.Birthdate = DateTime.Now;
                user.Gender = "Admin";
                var chack = userManager.Create(user, "@Hmed123");
                if (chack.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }

    }
}
