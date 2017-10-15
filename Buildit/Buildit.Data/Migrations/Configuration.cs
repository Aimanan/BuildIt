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

        //private void SeedSampleData(BuilditDbContext context)
        //{
        //    if (!context.Publications.Any())
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            var publ = new Publication()
        //            {
        //                Id = i,
        //                Title = "Saint-Laurent Sports Complex / Saucier + Perrotte architectes + HCMA ",
        //                Content = "From the architect. The project site is situated between the existing Émile Legault School and Raymond Bourque Arena, both of which are horizontal in form and neutral in character. For this project, it thus became vital for the design of new sports complex to create a visual and physical link between the Marcel Laurin Park (to the north of the site), and the projected green band that will run along Thimens Boulevard. The sculptural nature of the project creates a strong link between these two natural elements in the urban fabric.Two angular objects — one prismatic, white and diaphanous, the other darker and stretched horizontally — embrace the specific programmatic functions of the project but simultaneously transcend these,ninviting users and passersby from the boulevard, while serving as a signal for the passage toward the park beyond.",

        //                Author = "Saucier + Perrotte architectes + HCMA",
        //                PublishedOn = DateTime.Now,
        //                Description = "Sports Complex",
        //                Picture="NO PICTURE",

        //            };

        //            context.Publications.Add(publ);
        //        }

        //    }
        //}
    }
}



