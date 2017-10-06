using System.Linq;
using AnimalsShelter.Data.Model;

namespace AnimalsShelter.Services.Contracts
{
    public interface IAnimalsService
    {
        IQueryable<Animal> GetAll();
    }
}