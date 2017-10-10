using AnimalsShelter.Data.Model;
using AnimalsShelter.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using AnimalShelter.Common.Enums;
using System.ComponentModel;
using AnimalsShelter.Web.ViewModels;

namespace AnimalsShelter.Web.ViewModels
{
    public class AnimalsViewModel : IMapFrom<Animal>, IHaveCustomMappings, IAnimalsViewModel
    {
        public Guid Id { get; set; }
        
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

        [DataType(DataType.DateTime)]
        [DisplayName("Created On")]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Modified On")]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<AnimalsViewModel> Animals { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Animal, AnimalsViewModel>()
                .ForMember(viewModel => viewModel.ModifiedOn, cfg => cfg.MapFrom(model => model.CreatedOn));
        }
    }
}