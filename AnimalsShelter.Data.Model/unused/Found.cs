using AnimalsShelter.Data.Model.Abstracts;
using System.Collections.Generic;

namespace AnimalsShelter.Data.Model
{
    public class Found : DataModel
    {
        private ICollection<Animal> animals;

        public Found()
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
