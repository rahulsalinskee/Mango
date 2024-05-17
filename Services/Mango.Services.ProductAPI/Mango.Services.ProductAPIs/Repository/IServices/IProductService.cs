using Mango.Services.ProductAPIs.DTOs.CommonResponseDtos;
using Mango.Services.ProductAPIs.DTOs.ProductDtos;

namespace Mango.Services.ProductAPIs.Repository.IServices
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
        /// Update product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ResponseDto> UpdateProductByIdAsync(int id, ProductDto productDto);

        /// <summary>
        /// Update product by name
        /// </summary>
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
