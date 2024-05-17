using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart;

namespace Mango.Service.Shopping.Cart.API.Repository.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Get shopping cart information
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> CartUpdateInsertAsync(ShoppingCartDto shoppingCartDto);

        /// <summary>
        /// Delete shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> DeleteCartAsync(int shoppingCartDetailId);

        /// <summary>
        /// Get shopping cart information by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<ResponseDto> GetCartByUserIdAsync(string userId);

        /// <summary>
        /// Apply coupon to shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> ApplyCouponAsync(ShoppingCartDto shoppingCartDto);

        /// <summary>
        /// Remove coupon from shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> RemoveCouponAsync(ShoppingCartDto shoppingCartDto);
    }
}
