using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Tb_SupportType
    {
        [Key]
        public int Id { get; set; }

        public string SupportTypeName { get; set; }

        public string Description { get; set; }
    }
}
