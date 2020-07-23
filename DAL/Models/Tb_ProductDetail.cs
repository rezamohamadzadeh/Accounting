using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Tb_ProductDetail : BaseEntity
    {
        public string PlanKey { get; set; }
        public double Price { get; set; }
        public PayType Type { get; set; }

        public int ProductId { get; set; }
        public virtual Tb_Product Product { get; set; }


    }

}
