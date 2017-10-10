using AnimalsShelter.Data.Model.Abstracts;
using System.Collections.Generic;

namespace AnimalsShelter.Data.Model
{
    public class Shelter : DataModel
    {
        private ICollection<Animal> animals;

        public Shelter()
        {
            this.animals = new HashSet<Animal>();
        }

        public ICollection<Animal> Animals
        {
            get
            {
                return this.animals;
            }
            set
            {
                this.animals = value;
            }
        }
    }
}
