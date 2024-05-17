using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.CouponModels.DTOs;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CouponController : Controller
    {
        #region Private Data Members
        /// <summary>
        /// Private field
        /// </summary>
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the CouponController
        /// </summary>
        /// <param name="couponService"></param>
        public CouponController(ICouponService couponService)
        {
            this._couponService = couponService;
        }
        #endregion

        #region Get All Coupons Async
        /// <summary>
        /// Get all coupons
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CouponIndex()
        {
            IEnumerable<CouponDto>? coupons = new List<CouponDto>();

            ResponseDto? responseDto = await this._couponService.GetAllCouponsAsync();

            if (responseDto?.Result is not null && responseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Coupons Fetched Successfully!";
                coupons = JsonConvert.DeserializeObject<IEnumerable<CouponDto>>(Convert.ToString(responseDto?.Result));
            }
            else
            {
                TempData["ErrorMessage"] = responseDto?.DisplayMessage;
            }
            return View(coupons);
        }
        #endregion

        #region Create Coupon To Load UI - GET | POST
        /// <summary>
        /// Create coupon To Load the UI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        /// <summary>
        /// Create coupon
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                ResponseDto? newCouponResponseData = await this._couponService.CreateCouponAsync(couponDto: couponDto);

                if (newCouponResponseData?.Result is not null && newCouponResponseData.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Coupons Created Successfully!";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["ErrorMessage"] = newCouponResponseData?.DisplayMessage;
                }
            }
            return View(model: couponDto);
        }
        #endregion

        #region Coupon Delete By Id Async
        /// <summary>
        /// Delete coupon by id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CouponDeleteById([FromRoute] int couponId)
        {
            ResponseDto? couponResponseDto = await this._couponService.GetCouponByIdAsync(couponId: couponId);

            if (couponResponseDto?.Result is not null && couponResponseDto.IsSuccess)
            {
                var couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponseDto?.Result));
                TempData["SuccessMessage"] = "Coupons Deleted Successfully!";
                return View(couponDto);
            }
            else
            {
                TempData["ErrorMessage"] = couponResponseDto?.DisplayMessage;
            }
            return NotFound();
        }
        #endregion

        #region Coupon Delete By Name Async
        /// <summary>
        /// Delete coupon by id
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CouponDeleteById(CouponDto couponDto)
        {
            ResponseDto? couponResponseDto = await this._couponService.DeleteCouponByIdAsync(couponId: couponDto.CouponId);

            if (couponResponseDto?.Result is not null && couponResponseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Coupons Deleted Successfully!";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["ErrorMessage"] = couponResponseDto?.DisplayMessage;
            }
            return View(couponDto);
        } 
        #endregion
    }
}
