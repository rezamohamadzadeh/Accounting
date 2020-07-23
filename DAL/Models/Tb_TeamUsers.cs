using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    
    public class Tb_TeamUsers : BaseEntity
    {
        public Guid TeamId { get; set; }
        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Tb_Team Team { get; set; }
    }
}
