using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart;
using Mango.Service.Shopping.Cart.API.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mango.Service.Shopping.Cart.API.Controllers
{
    /// <summary>
    /// Cart API controller
    /// </summary>
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        #region Private Data Members
        /// <summary>
        /// Cart Service
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// User Id
        /// </summary>
        private const string USER_ID = "{userId}";
        #endregion

        #region Constructor
        // <summary>
        /// Constructor
        /// </summary>
        /// <param name="cartService"></param>
        public CartAPIController(ICartService cartService)
        {
            this._cartService = cartService;
        }
        #endregion

        #region Get Cart By User ID
        /// <summary>
        /// Get Cart By User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCartByUserId/" + USER_ID)]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await this._cartService.GetCartByUserIdAsync(userId: userId);
                return Ok(responseDto);
            }
            return BadRequest();
        }
        #endregion

        #region Apply Coupon
        /// <summary>
        /// Apply Coupon
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await this._cartService.ApplyCouponAsync(shoppingCartDto: shoppingCartDto);
                return Ok(responseDto);
            }
            return BadRequest(shoppingCartDto);
        }
        #endregion

        #region Remove Coupon
        /// <summary>
        /// Remove Coupon
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon([FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await this._cartService.RemoveCouponAsync(shoppingCartDto: shoppingCartDto);
                return Ok(responseDto);
            }
            return BadRequest(shoppingCartDto);
        } 
        #endregion

        #region Update Insert Cart
        /// <summary>
        /// Update and Insert the Cart Details
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upsert")]
        public async Task<IActionResult> UpdateInsert(ShoppingCartDto shoppingCartDto)
        {
            ResponseDto? responseDto = await this._cartService.CartUpdateInsertAsync(shoppingCartDto: shoppingCartDto);

            if (ModelState.IsValid)
            {
                if (responseDto != null && responseDto.IsSuccess)
                {
                    return Ok(responseDto);
                }
                return BadRequest(responseDto);
            }
            return BadRequest(responseDto);
        }
        #endregion

        #region Delete Product From Cart
        /// <summary>
        /// Delete the Cart
        /// </summary>
        /// <param name="shoppingCartDetailId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCart")]
        public async Task<IActionResult> DeleteCartHeader([FromBody] int shoppingCartDetailId)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await this._cartService.DeleteCartAsync(shoppingCartDetailId: shoppingCartDetailId);
                if (responseDto is not null && responseDto.IsSuccess)
                {
                    return Ok(responseDto);
                }
                return BadRequest(responseDto);
            }
            return BadRequest();
        }
        #endregion
    }
}
