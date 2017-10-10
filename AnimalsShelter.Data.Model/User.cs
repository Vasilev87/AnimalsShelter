using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.Model.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalsShelter.Data.Model
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IAuditable, IDeletable
    {
        //private ICollection<Shelter> shelterAnimals;
        //private ICollection<Rehome> rehomeAnimals;
        //private ICollection<Lost> lostAnimals;
        //private ICollection<Found> foundAnimals;
        private ICollection<IAnimal> animals;

        public User()
        {
            //this.shelterAnimals = new HashSet<Shelter>();
            //this.rehomeAnimals = new HashSet<Rehome>();
            //this.lostAnimals = new HashSet<Lost>();
            //this.foundAnimals = new HashSet<Found>();
            this.animals = new HashSet<IAnimal>();
        }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<IAnimal> Animals
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
        
        #region // collections
        //public virtual ICollection<Shelter> ShelterAnimals
        //{
        //    get
        //    {
        //        return this.shelterAnimals;
        //    }
        //    set
        //    {
        //        this.shelterAnimals = value;
        //    }
        //}

        //public virtual ICollection<Rehome> RehomeAnimals
        //{
        //    get
        //    {
        //        return this.rehomeAnimals;
        //    }
        //    set
        //    {
        //        this.rehomeAnimals = value;
        //    }
        //}

        //public virtual ICollection<Lost> LostAnimals
        //{
        //    get
        //    {
        //        return this.lostAnimals;
        //    }
        //    set
        //    {
        //        this.lostAnimals = value;
        //    }
        //}

        //public virtual ICollection<Found> FoundAnimals
        //{
        //    get
        //    {
        //        return this.foundAnimals;
        //    }
        //    set
        //    {
        //        this.foundAnimals = value;
        //    }
        //}
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
