using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimalsShelter.Data.Model.Contracts
{
    public interface IAdoptable
    {
        ICollection<Animal> Animals { get; set; }
    }
}
