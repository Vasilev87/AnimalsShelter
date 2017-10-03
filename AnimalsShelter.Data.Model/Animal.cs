using AnimalShelter.Common.Enums;
using AnimalsShelter.Data.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelter.Data.Model
{
    public class Animal : DataModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string Type { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string Breed { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public SizeType Size { get; set; }

        //public ShelterAnimal Shelter { get; set; }

        //public RehomeAnimal Rehome { get; set; }

        //public LostAnimal Lost { get; set; }

        //public FoundAnimal Found { get; set; }

        public virtual User User { get; set; }
    }
}
