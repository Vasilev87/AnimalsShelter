using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelerikAcademy.ForumSystem.Data.Repositories;

namespace AnimalsShelter.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IEfRepository<Animal> animalsRepo;

        public AnimalsService(IEfRepository<Animal> animalsRepo)
        {
            this.animalsRepo = animalsRepo;
        }

        public IQueryable<Animal> GetAll()
        {
            return this.animalsRepo.All;
        }
    }
}
