using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ApplicationUser>()
            .HasOne(a => a.BusinessOwner)
            .WithOne(b => b.User)
            .HasForeignKey<Tb_BusinessOwner>(b => b.UserId);

            builder.Entity<Tb_Product>()
            .HasIndex(u => u.ProductCode)
            .IsUnique();

            base.OnModelCreating(builder);
        }

        public DbSet<Tb_Sell> Tb_Sells { get; set; }

        public DbSet<Tb_Team> Tb_Teams { get; set; }

        public DbSet<Tb_Agent> Tb_Agents { get; set; }

        public DbSet<Tb_Broker> Tb_Brokers { get; set; }

        public DbSet<Tb_Factor> Tb_Factors { get; set; }

        public DbSet<Tb_Product> Tb_Product { get; set; }

        public DbSet<Tb_Support> Tb_Supports { get; set; }

        public DbSet<Tb_Setting> Tb_Settings { get; set; }

        public DbSet<Tb_TeamUsers> Tb_TeamUsers { get; set; }

        public DbSet<Tb_Affiliates> Tb_Affiliates { get; set; }                 
        
        public DbSet<Tb_SupportType> Tb_SupportTypes { get; set; }        

        public DbSet<Tb_ProductDetail> Tb_ProductDetails { get; set; }

        public DbSet<Tb_BusinessOwner> Tb_BusinessOwners { get; set; }

        public DbSet<Tb_ImportExcelData> Tb_ImportExcelDatas { get; set; }

        public DbSet<Tb_AffiliateReport> Tb_AffiliateReports { get; set; }

        public DbSet<Tb_AffiliateParameter> Tb_AffiliateParameters { get; set; }
    }
}
