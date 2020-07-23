using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Tb_BusinessOwner : BaseEntity
    {
        public Tb_BusinessOwner()
        {
            IsConfirm = false;
        }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string BusinessName { get; set; }
        public string Logo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        public string PayPalSecretKey { get; set; }
        public string PayPalClientId { get; set; }
        public string PayPalBusines { get; set; }
        public string StripPrivateKey { get; set; }
        public string StripePublishKey { get; set; }
        public string Certificate1 { get; set; }
        public string Certificate2 { get; set; }
        public string Certificate3 { get; set; }
        public bool IsConfirm { get; set; }

        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
    public enum PaymentType
    {
        PayPal = 1,
        Stripe = 2,
        Both = 3
    }
}
