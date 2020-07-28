using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public enum DatabaseName
    {
        [Display(Name = "BaseProj")]
        BaseProj,
        [Display(Name = "BuyerProfile")]
        BuyerProfile
    }
}
