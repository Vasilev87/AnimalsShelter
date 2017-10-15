namespace AnimalsShelter.Data.Migrations
{
    using AnimalsShelter.Common.Enums;
    using Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

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
                const string administratorRole = "Admin";
                const string regularUserRole = "User";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = new IdentityRole { Name = administratorRole };
                var userRole = new IdentityRole { Name = regularUserRole };

                roleManager.Create(adminRole);
                roleManager.Create(userRole);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPasswork);
                userManager.AddToRole(user.Id, administratorRole);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            var random = new Random();
            var names = new List<string>() { "Charlie", "Buddy", "Max", "Archie", "Oscar", "Toby", "Ollie", "Bailey", "Frankie", "Jack", "Bella", "Ruby", "Molly", "Lucy", "Coco", "Rosie", "Daisy", "Lola", "Tilly", "Bonnie", "Charlie", "Oscar", "Leo", "Max", "Ollie", "Milo", "Toby", "Jasper", "Shadow", "Simba", "Coco", "Bella", "Luna", "Molly", "Coco", "Lily", "Daisy", "Lucy", "Lulu", "Millie", "Peanut butter", "Teddy bear biscuit", "Albert Pamplemousse", "Apricot Strudel", "Baron Von Thumper", "Basil Fawlty", "Kransky", "Roxy RottenBottom", "Sir Benedict Cumberbatch", "Telly-Beyoncé", "Toffee Towers", "Tonks the Beautiful Lady", "Uggboot", "Zac Efron", "Mr Darcy Pumpkin", "Toothless", "Burger", "Gidget", "Kanye", "Laksa", "Lord Darth Vader", "Marakesh", "Mr Jinx", "Mr Schnitzel", "Princess Marshmallow", "Qantas" }; // 66
            var breeds = new List<string>() { "American Cocker Spaniel", "American Staffordshire Bull Terrier", "Anatolian Shepherd Dog", "Australian Cattle Dog", "Bulldogs", "Bullmastiff", "Bull Terrier", "Chihuahua", "Chinese", "Crested", "Chow Chow", "German Shepherd Dog", "German Short-Haired Pointer", "King Charles Spaniel", "Komondor", "Irish Setter", "Irish Terrier", "Irish Water Spaniel", "Welsh Corgi", "Balinese", "Bengal Cats", "Birman", "Bombay", "British Shorthair", "American Curl", "American Shorthair", "Colorpoint Shorthair", "Cornish Rex", "Egyptian Mau", "European Burmese", "Siamese Cat", "Siberian", "Singapura", "Senegal parrot", "Red-bellied parrot", "Cape parrot", "Yellow-fronted parrot" }; // 37

            if (!context.Animals.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var animal = new Animal()
                    {
                        Name = names[random.Next(0, 65)],
                        Type = AnimalType.Dog,
                        Breed = breeds[random.Next(0, 36)],
                        Gender = GenderType.Male,
                        Age = random.Next(1, 15),
                        Size = SizeType.Medium,
                        Address = "malinov " + i.ToString(),
                        PhoneNumber = "02202020",
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
                        Name = names[random.Next(0, 65)],
                        Type = AnimalType.Cat,
                        Breed = breeds[random.Next(0, 36)],
                        Gender = GenderType.Female,
                        Age = random.Next(1, 15),
                        Size = SizeType.Small,
                        Address = "malinov " + i.ToString(),
                        PhoneNumber = "02202020",
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now,
                        IsForRehome = true
                    };
                    context.Animals.Add(animal);
                }
                for (int i = 5; i < 10; i++)
                {
                    var animal = new Animal()
                    {
                        Name = names[random.Next(0, 65)],
                        Type = AnimalType.Cat,
                        Breed = breeds[random.Next(0, 36)],
                        Gender = GenderType.Female,
                        Age = random.Next(1, 15),
                        Size = SizeType.Small,
                        Address = "malinov " + i.ToString(),
                        PhoneNumber = "02202020",
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now,
                        IsLost = true
                    };
                    context.Animals.Add(animal);
                }
                for (int i = 5; i < 10; i++)
                {
                    var animal = new Animal()
                    {
                        Name = names[random.Next(0, 65)],
                        Type = AnimalType.Cat,
                        Breed = breeds[random.Next(0, 36)],
                        Gender = GenderType.Female,
                        Age = random.Next(1, 15),
                        Size = SizeType.Small,
                        Address = "malinov " + i.ToString(),
                        PhoneNumber = "02202020",
                        User = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now,
                        IsFound = true
                    };
                    context.Animals.Add(animal);
                }
            }
        }
    }
}
