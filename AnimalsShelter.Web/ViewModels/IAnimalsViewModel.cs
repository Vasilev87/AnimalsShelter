using AnimalsShelter.Common.Enums;
using System;

namespace AnimalsShelter.Web.Models.Animals
{
    public interface IAnimalsViewModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        AnimalType Type { get; set; }

        GenderType Gender { get; set; }

        int Age { get; set; }
        SizeType Size { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }
    }
}