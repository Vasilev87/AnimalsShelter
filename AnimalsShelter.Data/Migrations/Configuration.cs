namespace AnimalsShelter.Data.Migrations
{
    using AnimalShelter.Common.Enums;
    using AnimalsShelter.Data.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "admin@shelter.com";
        private const string AdministratorPasswork = "dadaqw";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);

            base.Seed(context);
        }

        private void SeedUsers(MsSqlDbContext context)
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

                userManager.Create(user, AdministratorPasswork);
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (!context.Animals.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var animal = new Animal()
                    {
                        Type = "Dog",
                        Breed = "Staff",
                        Gender = GenderType.Male,
                        Age = i + 2,
                        Size = SizeType.Small,
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Animals.Add(animal);
                }
            }
        }
    }
}
