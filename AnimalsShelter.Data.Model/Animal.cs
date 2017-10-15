using AnimalsShelter.Common.Enums;
using AnimalsShelter.Data.Model.Abstracts;
using AnimalsShelter.Data.Model.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnimalsShelter.Data.Model
{
    public class Animal : DataModel, IAnimal
    {
        [MinLength(2)]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        public AnimalType Type { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(45)]
        public string Breed { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public SizeType Size { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        //[Required]
        public string ImagePath { get; set; }

        [Display(Name = "Is for Adoption")]
        public Boolean IsForAdoption { get; set; }

        [Display(Name = "Is for Rehome")]
        public Boolean IsForRehome { get; set; }

        [Display(Name = "Is Lost")]
        public Boolean IsLost { get; set; }

        [Display(Name = "Is Found")]
        public Boolean IsFound { get; set; }

        public virtual User User { get; set; }
    }
}
