using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class ProfileDto
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "ProfileImage")]
        public IFormFile Files { get; set; }



    }
}
