using AnimalsShelter.Data.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AnimalsShelter.Data.Model.Abstracts
{
    public abstract class DataModel : IAuditable, IDeletable
    {
        public DataModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created On")]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Modified On")]
        public DateTime? ModifiedOn { get; set; }
    }
}
