using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

        public virtual Tb_BusinessOwner BusinessOwner { get; set; }

        public virtual List<Tb_TeamUsers> TeamUsers { get; set; } = new List<Tb_TeamUsers>();

        public virtual List<Tb_AffiliateReport> Tb_AffiliateReports { get; set; } = new List<Tb_AffiliateReport>();

    }
}
