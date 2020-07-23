using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Tb_Factor : BaseEntity
    {
        public Tb_Factor()
        {
            FactorCode = Guid.NewGuid().ToString().Substring(0, 5);
        }
        public string Price { get; set; }
        public PaymentType PaymentType { get; set; }        
        public PayFactorStatus PayFactorStatus { get; set; }
        public string FactorCode { get; set; }

        [ForeignKey("BusinessUser")]
        public string BusinessOwnerId { get; set; }

        [ForeignKey("AffiliateUser")]
        public string AffiliateId { get; set; }

        public ApplicationUser BusinessUser { get; set; }
        public ApplicationUser AffiliateUser { get; set; }

    }
    public enum PayFactorStatus
    {
        Success = 1,
        Pending = 2,
        Failed = 3
    }
}
