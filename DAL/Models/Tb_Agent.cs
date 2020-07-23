using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Tb_Agent
    {
        public Tb_Agent()
        {
            CreateDateTime = DateTime.Now;
            Code = Guid.NewGuid().ToString().Substring(0, 5);
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Company { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Code { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


    }
}
