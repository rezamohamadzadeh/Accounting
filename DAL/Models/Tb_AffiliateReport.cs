using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Tb_AffiliateReport : BaseEntity
    {
        public string Tell { get; set; }
        public CallState CallState { get; set; }
        public string Description { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Tb_Product Product { get; set; }

    }
    public enum CallState
    {
        Success = 1,
        Pending = 2,
        Failed = 3
    }
}
