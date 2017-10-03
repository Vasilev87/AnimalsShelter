﻿using AnimalsShelter.Data.Model.Abstracts;
using AnimalsShelter.Data.Model.Contracts;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelter.Data.Model
{
    public class Found : DataModel, IAdoptable
    {
        private ICollection<Animal> animals;

        public Found()
        {
            this.animals = new HashSet<Animal>();
        }

        public ICollection<Animal> Animals
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
    }
}
