using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Tb_ImportExcelData : BaseEntity
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Family")]
        public string Family { get; set; }

        [Display(Name = "Age")]
        public string Age { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
