using AnimalsShelter.Data.Repositories;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.SaveContext;
using AnimalsShelter.Services.Contracts;
using System.Linq;
using Bytes2you.Validation;

namespace AnimalsShelter.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly ISaveContext saveContext;

        public UsersService(IEfRepository<User> usersRepo, ISaveContext saveContext)
        {
            Guard.WhenArgument(usersRepo, nameof(usersRepo)).IsNull().Throw();
            Guard.WhenArgument(saveContext, nameof(saveContext)).IsNull().Throw();

            this.usersRepo = usersRepo;
            this.saveContext = saveContext;
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }
    }
}
