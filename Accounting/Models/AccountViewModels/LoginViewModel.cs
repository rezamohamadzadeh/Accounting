using System.ComponentModel.DataAnnotations;
namespace Accounting.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter valid {0}")]
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
