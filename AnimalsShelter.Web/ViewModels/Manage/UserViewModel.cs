using AnimalsShelter.Data.Model;
using AnimalsShelter.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalsShelter.Web.Models
{ 
    public class UserViewModel : IMapFrom<User>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string SecurityStamp { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ModifiedOn { get; set; }
    }
}