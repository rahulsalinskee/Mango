using Mango.Service.Shopping.Cart.API.DTOs.Coupon;

namespace Mango.Service.Shopping.Cart.API.Repository.Services
{
    public interface ICouponService
    {
        /// <summary>
        /// Get coupon by code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public Task<CouponDto> GetCouponByCodeAsync(string couponCode);
    }
}
