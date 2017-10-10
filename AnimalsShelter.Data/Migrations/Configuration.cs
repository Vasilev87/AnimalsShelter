namespace AnimalsShelter.Data.Migrations
{
    using AnimalShelter.Common.Enums;
    using Model;
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
                        Name = "Name" + i.ToString(),
                        Type = "Dog" + i.ToString(),
                        Breed = "Staff" + i.ToString(),
                        Gender = GenderType.Male,
                        Age = 1 + (uint)i,
                        Size = SizeType.Medium,
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now,
                        IsForAdoption = true
                    };
                    context.Animals.Add(animal);
                }
                for (int i = 5; i < 10; i++)
                {
                    var animal = new Animal()
                    {
                        Name = "Name" + 1.ToString(),
                        Type = "Cat" + i.ToString(),
                        Breed = "Britain",
                        Gender = GenderType.Female,
                        Age = (uint)i - 2,
                        Size = SizeType.Small,
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now,
                        IsForRehome = true
                    };
                    context.Animals.Add(animal);
                }
            }
        }
    }
}
