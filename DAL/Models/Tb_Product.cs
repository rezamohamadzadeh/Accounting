using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Tb_Product
    {
        public Tb_Product()
        {
            CreateDateTime = DateTime.Now;
            ProductCode = Guid.NewGuid().ToString().Substring(0, 5);
            Inventory = 0;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ProductCode { get; set; }
        public int Inventory { get; set; }

        public DateTime CreateDateTime { get; set; }        

        public List<Tb_AffiliateReport> Tb_AffiliateReports { get; set; } = new List<Tb_AffiliateReport>();
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual List<Tb_ProductDetail> Tb_ProductDetails { get; set; } = new List<Tb_ProductDetail>();
    }
    public enum PayType
    {
        Onece = 1,
        Monthly = 2,
        Yearly = 3,
        SixMonth = 4
    }
}
