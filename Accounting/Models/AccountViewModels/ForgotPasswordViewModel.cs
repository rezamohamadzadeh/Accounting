
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter valid {0}")]
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
