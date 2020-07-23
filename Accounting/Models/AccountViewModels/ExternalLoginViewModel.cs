
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
