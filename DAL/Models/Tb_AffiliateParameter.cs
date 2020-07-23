namespace DAL.Models
{
    public class Tb_AffiliateParameter : BaseEntity
    {
        public string Param1 { get; set; }
        public string Param1Val { get; set; }
        public string Param2 { get; set; }
        public string Param2Val { get; set; }
        public string Param3 { get; set; }
        public string Param3Val { get; set; }
        public string Param4 { get; set; }
        public string Param4Val { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
