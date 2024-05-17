using Mango.Services.Coupon.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Coupon.ApplicationDataContext.ApplicationDataContext
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor for the ApplicationDbContext
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var relationalDatabaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (relationalDatabaseCreator is not null)
                {
                    if (!relationalDatabaseCreator.Exists())
                    {
                        relationalDatabaseCreator.Create();
                    }
                    if (!relationalDatabaseCreator.HasTables())
                    {
                        relationalDatabaseCreator.Create();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error Message: {exception.Message}");
            }
        }

        /// <summary>
        /// Table [Table Name: TblCoupons]
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* This Coupon record will be created in Database [Table Name: TblCoupons] */
            modelBuilder.Entity<CouponModel>().HasData(new CouponModel() 
            { 
                CouponId = 1, 
                CouponCode = "10OFF", 
                DiscountAmount = 10, 
                MinimumAmount = 100, 
                CreatedDate = DateTime.Now.AddDays(2), 
                IsCouponActive = true, 
                ExpiryDate = DateTime.Now.AddDays(4) 
            });

            modelBuilder.Entity<CouponModel>().HasData(new CouponModel() 
            { 
                CouponId = 2, 
                CouponCode = "20OFF", 
                DiscountAmount = 20, 
                MinimumAmount = 200, 
                CreatedDate = DateTime.Now.AddDays(2), 
                IsCouponActive = true, 
                ExpiryDate = DateTime.Now.AddDays(4) 
            });
        }

        /// <summary>
        /// Table [Table Name: TblCoupons]
        /// </summary>
        public DbSet<CouponModel> TblCoupons { get; set; }
    }
}
