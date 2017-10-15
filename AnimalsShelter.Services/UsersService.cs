using AnimalsShelter.Data.Repositories;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.SaveContext;
using AnimalsShelter.Services.Contracts;
using System.Linq;

namespace AnimalsShelter.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly ISaveContext saveContext;

        public UsersService(IEfRepository<User> usersRepo, ISaveContext saveContext)
        {
            this.usersRepo = usersRepo;
            this.saveContext = saveContext;
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }
    }
}
