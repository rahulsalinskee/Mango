using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.Coupon.ApplicationDataContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountedAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "MinimumAmount",
                value: 100);

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "MinimumAmount",
                value: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "MinimumAmount",
                value: 10);

            migrationBuilder.UpdateData(
                table: "TblCoupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "MinimumAmount",
                value: 20);
        }
    }
}
