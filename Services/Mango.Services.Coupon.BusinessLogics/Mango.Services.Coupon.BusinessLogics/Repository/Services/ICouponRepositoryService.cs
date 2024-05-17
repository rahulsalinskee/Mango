using Mango.Services.Coupon.Model.DTOs.CommonResponseDtos;
using Mango.Services.Coupon.Model.DTOs.CouponDtos;
using Mango.Services.Coupon.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Coupon.BusinessLogics.Repository.Services
{
    public interface ICouponRepositoryService
    {
        /// <summary>
        /// Get all coupons
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto> GetAllCouponsAsync();

        /// <summary>
        /// Get coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        Task<ResponseDto> GetCouponByIdAsync(int couponId);

        /// <summary>
        /// Get coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        Task<ResponseDto> GetCouponByCodeAsync(string couponCode);

        /// <summary>
        /// Create coupon
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        Task<ResponseDto> CreateCouponAsync(CouponDto couponDto);

        /// <summary>
        /// Update coupon
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        Task<ResponseDto> UpdateCouponByIdAsync(int couponId, CouponDto couponDto);

        /// <summary>
        /// Update coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        Task<ResponseDto> UpdateCouponByCodeAsync(string couponCode, CouponDto couponDto);

        /// <summary>
        /// Delete coupon by coupon code
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        Task<ResponseDto> DeleteCouponByIdAsync(int couponId);

        /// <summary>
        /// Delete coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        Task<ResponseDto> DeleteCouponByCodeAsync(string couponCode);
    }
}
