using AnimalShelter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelter.Data.Model.Contracts
{
    public interface IAnimal
    {
        string Name { get; set; }

        string Type { get; set; }

        string Breed { get; set; }

        GenderType Gender { get; set; }

        uint Age { get; set; }

        SizeType Size { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }

        Boolean IsForAdoption { get; set; }

        Boolean IsForRehome { get; set; }

        Boolean IsLost { get; set; }

        Boolean IsFound { get; set; }
    }
}
