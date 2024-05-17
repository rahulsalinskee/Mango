﻿// <auto-generated />
using System;
using Mango.Services.Coupon.ApplicationDataContext.ApplicationDataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.Coupon.ApplicationDataContext.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240321124733_AddCouponCreatedDate_AddExpiryCouponDate")]
    partial class AddCouponCreatedDate_AddExpiryCouponDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.Coupon.Model.Models.CouponModel", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCouponActive")
                        .HasColumnType("bit");

                    b.Property<int>("MinimumAmount")
                        .HasColumnType("int");

                    b.HasKey("CouponId");

                    b.ToTable("TblCoupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1,
                            CouponCode = "10OFF",
                            CreatedDate = new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5069),
                            DiscountAmount = 10.0,
                            ExpiryDate = new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5088),
                            IsCouponActive = true,
                            MinimumAmount = 100
                        },
                        new
                        {
                            CouponId = 2,
                            CouponCode = "20OFF",
                            CreatedDate = new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135),
                            DiscountAmount = 20.0,
                            ExpiryDate = new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135),
                            IsCouponActive = true,
                            MinimumAmount = 200
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
