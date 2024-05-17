using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.Coupon.ApplicationDataContext.Migrations
{
    /// <inheritdoc />
    public partial class SettingIdentityForCouponIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 3, 25, 16, 29, 42, 612, DateTimeKind.Local).AddTicks(3956), new DateTime(2024, 3, 27, 16, 29, 42, 612, DateTimeKind.Local).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 3, 25, 16, 29, 42, 612, DateTimeKind.Local).AddTicks(4034), new DateTime(2024, 3, 27, 16, 29, 42, 612, DateTimeKind.Local).AddTicks(4036) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5069), new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5088) });

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 3, 23, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135), new DateTime(2024, 3, 25, 18, 17, 32, 757, DateTimeKind.Local).AddTicks(5135) });
        }
    }
}
