using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.CouponModels.DTOs;

namespace Mango.Web.Services.IService
{
    public interface ICouponService
    {
        /// <summary>
        /// Get all coupons
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto?> GetAllCouponsAsync();

        /// <summary>
        /// Get coupon by code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);

        /// <summary>
        /// Get coupon by id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetCouponByIdAsync(int couponId);

        /// <summary>
        /// Create coupon async
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);

        /// <summary>
        /// Update coupon by ID
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        Task<ResponseDto?> UpdateCouponByIdAsync(int couponId, CouponDto couponDto);

        /// <summary>
        /// Update coupon by code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        Task<ResponseDto?> UpdateCouponByCouponCodeAsync(string couponCode, CouponDto couponDto);

        /// <summary>
        /// Delete coupon by code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        Task<ResponseDto?> DeleteCouponByCodeAsync(string couponCode);

        /// <summary>
        /// Delete coupon by ID
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        Task<ResponseDto?> DeleteCouponByIdAsync(int couponId);
    }
}
