using AnimalsShelter.Data.Model;
using System.Linq;

namespace AnimalsShelter.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> GetAll();
    }
}
