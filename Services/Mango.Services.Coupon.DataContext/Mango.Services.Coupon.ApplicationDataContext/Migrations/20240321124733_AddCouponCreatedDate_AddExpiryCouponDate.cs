using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.Coupon.ApplicationDataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponCreatedDate_AddExpiryCouponDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblCoupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "TblCoupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCouponActive",
                table: "TblCoupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ExpiryDate", "IsCouponActive" },
                values: new object[] { new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5069), new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5088), true });

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ExpiryDate", "IsCouponActive" },
                values: new object[] { new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135), new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblCoupons");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "TblCoupons");

            migrationBuilder.DropColumn(
                name: "IsCouponActive",
                table: "TblCoupons");
        }
    }
}
