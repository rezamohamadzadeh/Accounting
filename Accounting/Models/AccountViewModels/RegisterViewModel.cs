using System.ComponentModel.DataAnnotations;

namespace Accounting.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا فیلد {0} را وارد کنید")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "لطفا ایمیل را بدرستی وارد کنید")]
        [Required(ErrorMessage = "لطفا فیلد {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا فیلد {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "فیلد {0} باید بین {2} و {1} کاراکتر طول داشته باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور با تایید کلمه عبور همخوانی ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
