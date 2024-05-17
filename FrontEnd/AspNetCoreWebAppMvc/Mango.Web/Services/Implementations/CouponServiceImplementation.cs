using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.CouponModels.DTOs;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;

namespace Mango.Web.Services.Implementations
{
    public class CouponServiceImplementation : ICouponService
    {
        #region Private Data Members
        /// <summary>
        /// Private Data Members
        /// </summary>
        private readonly IBaseService _baseService;

        /// <summary>
        /// Coupon Controller End Point
        /// Here, coupon is the controller name
        /// </summary>
        private const string COUPON_CONTROLLER_API_END_POINT = "/api/coupon";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the CouponService
        /// </summary>
        /// <param name="baseService"></param>
        public CouponServiceImplementation(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        #endregion

        #region Create Coupon Async
        /// <summary>
        /// Create CouponAsync
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = couponDto,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT,
            });
        }
        #endregion

        #region Get All Coupons Async
        /// <summary>
        /// Get All Coupon
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT,
            });
        }
        #endregion

        #region Get Coupon by coupon code Async
        /// <summary>
        /// Get Coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/GetCouponByCode/" + couponCode,
            });
        }
        #endregion

        #region Get Coupon by coupon id Async
        /// <summary>
        /// Get Coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/GetCouponById/" + couponId,
            });
        }
        #endregion

        #region Delete Coupon by coupon code Async
        /// <summary>
        /// Delete Coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto?> DeleteCouponByCodeAsync(string couponCode)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/DeleteCouponByCode/" + couponCode,
            });
        }
        #endregion

        #region Delete Coupon by coupon id Async
        /// <summary>
        /// Delete Coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> DeleteCouponByIdAsync(int couponId)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/DeleteCouponById/" + couponId,
            });
        }
        #endregion

        #region Update Coupon by coupon code Async
        /// <summary>
        /// Update Coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto?> UpdateCouponByCouponCodeAsync(string couponCode, CouponDto couponDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/UpdateCouponByCouponCode/" + couponCode,
                Data = couponDto
            });
        }
        #endregion

        #region Update Coupon by coupon id Async
        /// <summary>
        /// Update Coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpdateCouponByIdAsync(int couponId, CouponDto couponDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.CouponAPIBaseURL + COUPON_CONTROLLER_API_END_POINT + "/UpdateCouponById/" + couponId,
                Data = couponDto
            });
        }
        #endregion
    }
}
