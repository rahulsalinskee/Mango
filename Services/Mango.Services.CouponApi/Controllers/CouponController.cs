using AutoMapper;
using Mango.Services.Coupon.BusinessLogics.Repository.Services;
using Mango.Services.Coupon.Model.DTOs.CouponDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CouponController : ControllerBase
    {
        #region Private Data Members
        private const string INTEGER_ID = "{id:int}";
        private const string STRING_COUPON_CODE = "{couponCode}";
        private const string ROLE_ADMIN = "ADMIN";
        private readonly ICouponRepositoryService _couponRepositoryService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for CouponController
        /// </summary>
        /// <param name="couponRepositoryService"></param>
        public CouponController(ICouponRepositoryService couponRepositoryService)
        {
            this._couponRepositoryService = couponRepositoryService;
        }
        #endregion

        #region Get All Coupons Async
        /// <summary>
        /// Get All Coupons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCouponsAsync()
        {
            var coupons = await this._couponRepositoryService.GetAllCouponsAsync();
            if (coupons is not null)
            {
                return Ok(coupons);
            }
            return NotFound();
        }
        #endregion

        #region Get Coupon By Id Async
        /// <summary>
        /// Get Coupon By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCouponById/" + INTEGER_ID)]
        public async Task<IActionResult> GetCouponById([FromRoute] int id)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                var coupon = await this._couponRepositoryService.GetCouponByIdAsync(couponId: id);
                if (coupon.Result is not null)
                {
                    return Ok(coupon);
                }
                return NotFound($"No Coupon Found with Id: {id}!");
            }
            return NotFound(ModelState);
        }
        #endregion

        #region Get Coupon By Code Async
        /// <summary>
        /// Get Coupon By Code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCouponByCode/" + STRING_COUPON_CODE)]
        public async Task<IActionResult> GetCouponByCodeAsync(string couponCode)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                var coupon = await this._couponRepositoryService.GetCouponByCodeAsync(couponCode: couponCode);

                if (coupon is not null)
                {
                    return Ok(couponCode);
                }
                return NotFound($"No Coupon Found with coupon code: {coupon}!");
            }
            return NotFound(ModelState);
        }
        #endregion

        #region Create Coupon Async
        /// <summary>
        /// Create Coupon Code
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = ROLE_ADMIN)]
        public async Task<IActionResult> CreateCouponAsync([FromBody] CouponDto couponDto)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                var coupon = await this._couponRepositoryService.CreateCouponAsync(couponDto);
                if (coupon is not null)
                {
                    return Ok(coupon);
                }
                return BadRequest($"Coupon: {couponDto.CouponCode} could not be created!");
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Update Coupon By ID Async
        /// <summary>
        /// Update Coupon
        /// </summary>
        /// <param name="id"></param>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = ROLE_ADMIN)]
        [Route("UpdateCouponById/" + INTEGER_ID)]
        public async Task<IActionResult> UpdateCouponByIdAsync([FromRoute] int id, [FromBody] CouponDto couponDto)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                if (couponDto is not null)
                {

                    var updatedCouponDto = await this._couponRepositoryService.UpdateCouponByIdAsync(couponId: id, couponDto: couponDto);
                    if (updatedCouponDto.Result is not null)
                    {
                        return Ok(updatedCouponDto);
                    }
                    return BadRequest($"Coupon: {couponDto.CouponCode} could not be updated!");
                }
                return NotFound($"Coupon: {couponDto.CouponCode} not found!");
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Update Coupon By Code Async
        /// <summary>
        /// Update Coupon
        /// </summary>
        /// <param name="id"></param>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = ROLE_ADMIN)]
        [Route("UpdateCouponByCouponCode/" + STRING_COUPON_CODE)]
        public async Task<IActionResult> UpdateCouponByCouponCodeAsync(string couponCode, [FromBody] CouponDto couponDto)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                if (couponDto is not null)
                {
                    var updatedCouponDto = await this._couponRepositoryService.UpdateCouponByCodeAsync(couponCode: couponCode, couponDto: couponDto);
                    if (updatedCouponDto.Result is not null)
                    {
                        return Ok(updatedCouponDto);
                    }
                    return NotFound($"Coupon: {couponCode} not found!");
                }
                return BadRequest($"Coupon: {couponDto.CouponCode} could not be updated!");
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete Coupon By Id Async
        /// <summary>
        /// Delete Coupon By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = ROLE_ADMIN)]
        [Route("DeleteCouponById/" + INTEGER_ID)]
        public async Task<IActionResult> DeleteCouponById(int id)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                var isCouponDeleted = await this._couponRepositoryService.DeleteCouponByIdAsync(couponId: id);
                if (isCouponDeleted.IsSuccess)
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete Coupon By Code Async
        /// <summary>
        /// Delete Coupon By Code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = ROLE_ADMIN)]
        [Route("DeleteCouponByCode/" + STRING_COUPON_CODE)]
        public async Task<IActionResult> DeleteCouponByCodeAsync(string couponCode)
        {
            /* ModelState.IsValid is a server side validation & is always recommended to do that validation */
            if (ModelState.IsValid)
            {
                var isCouponDeleted = await this._couponRepositoryService.DeleteCouponByCodeAsync(couponCode: couponCode);
                if (isCouponDeleted.IsSuccess)
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
