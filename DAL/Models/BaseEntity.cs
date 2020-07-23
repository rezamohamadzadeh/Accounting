using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
  
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Create DateTime")]
        public DateTime CreateDateTime { get; set; }

        public BaseEntity()
        {
            CreateDateTime = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }


}
