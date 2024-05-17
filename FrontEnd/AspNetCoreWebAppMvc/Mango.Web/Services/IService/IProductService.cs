
using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.ProductModels.DTOs;

namespace Mango.Web.Services.IService
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto> GetProductsAsync();

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ResponseDto> GetProductByIdAsync(int productId);

        /// <summary>
        /// Get product by name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<ResponseDto> GetProductByNameAsync(string productName);

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ResponseDto> CreateProductAsync(ProductDto productDto);

        /// <summary>
        /// Update product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ResponseDto> UpdateProductByIdAsync(int productId, ProductDto productDto);

        /// <summary>
        /// Updates the specified product by name.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ResponseDto> UpdateProductByNameAsync(string productName, ProductDto productDto);

        /// <summary>
        /// Update product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ResponseDto> DeleteProductByIdAsync(int productId);

        /// <summary>
        /// Update product by name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<ResponseDto> DeleteProductByNameAsync(string productName);
    }
}
