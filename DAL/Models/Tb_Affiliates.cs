using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
   public class Tb_Affiliates
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string phone { get; set; }
        public string Company { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string CreateAt { get; set; }
        public string Code { get; set; }
        public int Click { get; set; }
        public int comision { get; set; }
    }
}
