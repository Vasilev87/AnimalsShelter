using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using System.Linq;
using AnimalsShelter.Data.Repositories;
using AnimalsShelter.Data.SaveContext;
using Bytes2you.Validation;

namespace AnimalsShelter.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IEfRepository<Animal> animalsRepo;
        private readonly ISaveContext saveContext;

        public AnimalsService(IEfRepository<Animal> animalsRepo, ISaveContext saveContext)
        {
            Guard.WhenArgument(animalsRepo, nameof(animalsRepo)).IsNull().Throw();
            Guard.WhenArgument(saveContext, nameof(saveContext)).IsNull().Throw();

            this.animalsRepo = animalsRepo;
            this.saveContext = saveContext;
        }

        public void Add(Animal animal)
        {
            this.animalsRepo.Add(animal);
            this.saveContext.Commit();
        }

        public void Update(Animal animal)
        {
            this.animalsRepo.Update(animal);
            this.saveContext.Commit();
        }

        public void Delete(Animal animal)
        {
            this.animalsRepo.Delete(animal);
            this.saveContext.Commit();
        }

        public IQueryable<Animal> GetAll()
        {
            return this.animalsRepo.All;
        }
    }
}
