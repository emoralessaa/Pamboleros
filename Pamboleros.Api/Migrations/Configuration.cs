namespace Pamboleros.Api.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Pamboleros.Api.Infrastructure;
    using Pamboleros.Api.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pamboleros.Api.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Pamboleros.Api.Infrastructure.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "SuperAdmin",
                Email = "emorales.s@hotmail.com",
                EmailConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                Level = 1,
                JoinDate = DateTime.Now
            };

            manager.Create(user, "Pambinhos");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("SuperAdmin");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
        }
    }
}
