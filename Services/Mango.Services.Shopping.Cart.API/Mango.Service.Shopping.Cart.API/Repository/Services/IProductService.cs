using Mango.Service.Shopping.Cart.API.DTOs.Product;

namespace Mango.Service.Shopping.Cart.API.Repository.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Get All Products Async
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
