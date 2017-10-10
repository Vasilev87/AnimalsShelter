using AnimalShelter.Common.Enums;
using AnimalsShelter.Data.Model.Abstracts;
using AnimalsShelter.Data.Model.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelter.Data.Model
{
    public class Animal : DataModel, IAnimal
    {
        [MinLength(2)]
        [MaxLength(15)]
        public string Name { get; set; }

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
        public uint Age { get; set; }

        [Required]
        public SizeType Size { get; set; }

        [Required]  
        public string Address { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }

        [DisplayName("Is for Adoption")]
        public Boolean IsForAdoption { get; set; }

        [DisplayName("Is for Rehome")]
        public Boolean IsForRehome { get; set; }

        [DisplayName("Is Lost")]
        public Boolean IsLost { get; set; }

        [DisplayName("Is Found")]
        public Boolean IsFound { get; set; }

        public virtual User User { get; set; }
    }
}
