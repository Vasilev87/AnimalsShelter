using AnimalShelter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalsShelter.Web.ViewModels
{
    public interface IAnimalsViewModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Type { get; set; }

        GenderType Gender { get; set; }

        uint Age { get; set; }
        SizeType Size { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }
    }
}