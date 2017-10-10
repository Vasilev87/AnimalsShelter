using System.Linq;
using AnimalsShelter.Data.Model;

namespace AnimalsShelter.Services.Contracts
{
    public interface IAnimalsService
    {
        void Add(Animal animal);

        void Update(Animal animal);

        void Delete(Animal animal);

        IQueryable<Animal> GetAll();
    }
}