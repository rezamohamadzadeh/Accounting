using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Tb_Team : BaseEntity
    {
        public Tb_Team()
        {
            TeamCode = Guid.NewGuid().ToString().Substring(0, 5);
        }

        [Display(Name = "Team Image")]
        //[Required(ErrorMessage = "Please Enter {0}")]
        public string Image { get; set; }

        [Display(Name = "Team Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string Name { get; set; }

        [Display(Name = "Team Description")]
        public string Description { get; set; }

        [Display(Name = "Team Code")]
        public string TeamCode { get; set; }

        [Display(Name = "Team Leader")]
        public string TeamLeader { get; set; }

        public string ProductCode { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual List<Tb_TeamUsers> TeamUsers { get; set; } = new List<Tb_TeamUsers>();

    }
}
