using AnimalsShelter.Data.Model.Abstracts;
using System.Collections.Generic;

namespace AnimalsShelter.Data.Model
{
    public class Rehome : DataModel
    {
        private ICollection<Animal> animals;

        public Rehome()
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
