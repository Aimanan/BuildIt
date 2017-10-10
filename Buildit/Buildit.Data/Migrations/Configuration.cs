namespace Buildit.Data.Migrations
{
    using Buildit.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Buildit.Data.BuilditDbContext>
    {
        private const string AdministratorUserName = "pesho.com";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BuilditDbContext context)
        {
            this.SeedUsers(context);
            //this.SeedSampleData(context);

            base.Seed(context);

        }

        private void SeedUsers(BuilditDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleName = "Admin";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}




//        private void SeedSampleData(BuilditDbContext context)
//        {
//            if (!context.Publications.Any())
//            {
//                for (int i = 0; i < 5; i++)
//                {
//                    var post = new Publication()
//                    {
//                        Title = "Post " + i,
//                        Content = "Text sample blq blq blq",
//                        Author = context.Users.First(x => x.Email == AdministratorUserName),
//                        //CreatedOn = DateTime.Now
//                    };

//                    context.Publication.Add(post);
//                }
//            }
//        }
//    }
//}