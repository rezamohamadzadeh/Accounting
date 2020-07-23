using System.ComponentModel.DataAnnotations;

namespace Accounting.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter valid {0}")]
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [StringLength(100, ErrorMessage = "{0} must be {2} minimum length and {1} maximum length", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The passwords are not match together")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    
}
