using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.ProductModels.DTOs;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Services.Implementations
{
    /// <summary>
    /// Product Service Implementation By Consuming Product API
    /// </summary>
    public class ProductServiceImplementation : IProductService
    {
        #region Private Members
        /// <summary>
        /// Declare IBaseService
        /// </summary>
        private readonly IBaseService _baseService;

        /// <summary>
        /// Product Controller API End Point
        /// </summary>
        private const string PRODUCT_CONTROLLER_API_END_POINT = "/api/product";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="responseDto"></param>
        /// <param name="mapper"></param>
        public ProductServiceImplementation(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        #endregion

        #region Create Product Async By Consuming Product API
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = productDto,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT,
            });
        }
        #endregion

        #region Delete Product By Id Async By Consuming Product API
        /// <summary>
        /// Delete Product By Id Async
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteProductByIdAsync(int productId)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/DeleteProductById/" + productId,
            });
        }
        #endregion

        #region Delete Product By Name Async By Consuming Product API
        /// <summary>
        /// Delete Product By Name Async 
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteProductByNameAsync(string productName)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/DeleteProductByName/" + productName,
            });
        } 
        #endregion

        #region Get Product By Id Async By Consuming Product API
        /// <summary>
        /// Get Product By Id Async
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductByIdAsync(int productId)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/GetProductById/" + productId,
            });
        }
        #endregion

        #region Get Product By Name Async By Consuming Product API
        /// <summary>
        /// Get Product By Name Async
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductByNameAsync(string productName)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/GetProductByName/" + productName,
            });
        } 
        #endregion

        #region Get All Products Async By Consuming Product API
        /// <summary>
        /// Get Products By consuming Product API
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductsAsync()
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/GetAllProducts",
            });
        }
        #endregion

        #region Update Product By Id Async By Consuming Product API
        /// <summary>
        /// Update Product By Id Async
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateProductByIdAsync(int productId, ProductDto productDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/UpdateProductById/" + productId,
                Data = productDto
            });
        }

        /// <summary>
        /// Updates the specified product by name Async.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateProductByNameAsync(string productName, ProductDto productDto)
        {
            return await this._baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.ProductAPIBaseURL + PRODUCT_CONTROLLER_API_END_POINT + "/UpdateProductByName/" + productName,
                Data = productDto
            });
        }
        #endregion
    }
}
