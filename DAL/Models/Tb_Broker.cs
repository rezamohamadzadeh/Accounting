using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Tb_Broker
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        

    }
}
