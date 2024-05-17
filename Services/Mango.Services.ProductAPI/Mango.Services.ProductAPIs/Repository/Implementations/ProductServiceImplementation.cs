using AutoMapper;
using Mango.Services.ProductAPIs.ApplicationDataContext;
using Mango.Services.ProductAPIs.DTOs.CommonResponseDtos;
using Mango.Services.ProductAPIs.DTOs.ProductDtos;
using Mango.Services.ProductAPIs.Models;
using Mango.Services.ProductAPIs.Repository.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPIs.Repository.Implementations
{
    public class ProductServiceImplementation : IProductService
    {
        #region Private Members
        /// <summary>
        /// Application DB Context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Response DTO
        /// </summary>
        private readonly ResponseDto _responseDto;

        /// <summary>
        /// Mapper for converting Product Model to Product DTO and vice versa
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="responseDto"></param>
        /// <param name="mapper"></param>
        public ProductServiceImplementation(ApplicationDbContext applicationDbContext, ResponseDto responseDto, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = responseDto;
            _mapper = mapper;
        }
        #endregion

        #region Create Product Async
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> CreateProductAsync([FromBody] ProductDto productDto)
        {
            try
            {
                /* Need to convert Product DTO to Product Model */
                ProductModel productModel = _mapper.Map<ProductModel>(source: productDto);

                if (productModel is not null)
                {
                    await _applicationDbContext.TblProducts.AddAsync(productModel);
                    await _applicationDbContext.SaveChangesAsync();

                    /* Need to convert Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<ProductDto>(source: productModel);
                    _responseDto.IsSuccess = true;
                    _responseDto.DisplayMessage = "Product created successfully!";
                }
                else
                {
                    _responseDto.Result = null;
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "Error happened while creating product";
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
            }
            return _responseDto;
        }
        #endregion

        #region Delete Product By Product ID Async
        /// <summary>
        /// Delete Product by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteProductByIdAsync(int productId)
        {
            try
            {
                var fetchedProductModel = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.ProductId == productId);
                if (fetchedProductModel is not null)
                {
                    _applicationDbContext.TblProducts.Remove(fetchedProductModel);
                    await _applicationDbContext.SaveChangesAsync();
                    _responseDto.IsSuccess = true;

                    /* Need Mapper to convert Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<ProductDto>(source: fetchedProductModel);
                    _responseDto.DisplayMessage = "Product Deleted Successfully!";
                }
                else
                {
                    _responseDto.Result = null;
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "Product Not Found!";
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
            }
            return _responseDto;
        }
        #endregion

        #region Delete Product By Product Name Async
        /// <summary>
        /// Delete Product by Product Name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteProductByNameAsync(string productName)
        {
            try
            {
                var fetchedProducModeltByName = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.Name == productName);

                if (fetchedProducModeltByName is not null)
                {
                    _applicationDbContext.TblProducts.Remove(fetchedProducModeltByName);
                    await _applicationDbContext.SaveChangesAsync();
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = _mapper.Map<ProductDto>(source: fetchedProducModeltByName);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = null;
                    _responseDto.DisplayMessage = $"No such Product Found By {productName}";
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
            }
            return _responseDto;
        }
        #endregion

        #region Get Product By ID
        /// <summary>
        /// Get Product by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductByIdAsync(int productId)
        {
            ProductModel? productModel = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.ProductId == productId);

            if (productModel != null)
            {
                _responseDto.IsSuccess = true;

                /* Need Mapper to convert Product Model to Product DTO */
                _responseDto.Result = _mapper.Map<ProductDto>(source: productModel);
                _responseDto.DisplayMessage = "Product Fetched Successfully!";
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.Result = null;
            }
            return _responseDto;
        }
        #endregion

        #region Get Product By Product Name Async
        /// <summary>
        /// Get the product by product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductByNameAsync(string productName)
        {
            try
            {
                ProductModel? productModel = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.Name == productName);

                if (productModel != null)
                {
                    _responseDto.IsSuccess = true;

                    /* Need Mapper to convert Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<ProductDto>(source: productModel);
                    _responseDto.DisplayMessage = "Product Fetched By Its Name Successfully!";
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = null;
                    _responseDto.DisplayMessage = $"No such Product Found By {productName}";
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
                _responseDto.Result = null;
            }
            return _responseDto;
        }
        #endregion

        #region Get All Products Async
        /// <summary>
        /// Gets all products Async
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto> GetProductsAsync()
        {
            try
            {
                var fetchedProductModel = await _applicationDbContext.TblProducts.AsNoTracking().ToListAsync();

                if (fetchedProductModel.Count >= 1)
                {
                    _responseDto.DisplayMessage = $"{fetchedProductModel.Count} Are Available";
                    _responseDto.IsSuccess = true;

                    /* Need Mapper to convert Collection of Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(source: fetchedProductModel); ;
                }
                else
                {
                    _responseDto.DisplayMessage = "No products available";
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = null;
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
                _responseDto.Result = null;
            }
            return _responseDto;
        }
        #endregion

        #region Update Product By Product ID Async
        /// <summary>
        /// Update Product by Product ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateProductByIdAsync(int id, ProductDto productDto)
        {
            try
            {
                /* Fetch the existing product using id from database */
                var fetchedProductModel = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.ProductId == id);

                if (fetchedProductModel?.ProductId == id && fetchedProductModel is not null)
                {
                    /* Update the existing product details with new values */
                    fetchedProductModel.Name = productDto.Name;
                    fetchedProductModel.Price = productDto.Price;
                    fetchedProductModel.Description = productDto.Description;
                    fetchedProductModel.ImageUrl = productDto.ImageUrl;
                    fetchedProductModel.CategoryName = productDto.CategoryName;

                    /* Save the changes */
                    await _applicationDbContext.SaveChangesAsync();

                    /* Need Mapper to convert Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<ProductDto>(source: fetchedProductModel);
                    _responseDto.IsSuccess = true;
                    _responseDto.DisplayMessage = "Product Updated Successfully!";
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Result = null;
                    _responseDto.DisplayMessage = $"No such Product Found By {id}";
                }
            }
            catch (Exception exception)
            {
                _responseDto.Result = null;
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = exception.Message;
            }
            return _responseDto;
        }
        #endregion

        #region Update Product By Product Name Async
        /// <summary>
        /// Update Product by Product Name
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateProductByNameAsync(string productName, ProductDto productDto)
        {
            try
            {
                var fetchedExistingProductByName = await _applicationDbContext.TblProducts.FirstOrDefaultAsync(product => product.Name == productName);

                if (fetchedExistingProductByName == null)
                {
                    _responseDto.Result = null;
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = $"No such Product Found By {productName}";
                }
                else
                {
                    if (fetchedExistingProductByName.Name == productName)
                    {
                        fetchedExistingProductByName.Name = productDto.Name;
                        fetchedExistingProductByName.Price = productDto.Price;
                        fetchedExistingProductByName.Description = productDto.Description;
                    }
                    await _applicationDbContext.SaveChangesAsync();
                    _responseDto.IsSuccess = true;

                    /* Need Mapper to convert Product Model to Product DTO */
                    _responseDto.Result = _mapper.Map<ProductDto>(source: fetchedExistingProductByName);
                    _responseDto.DisplayMessage = "Product Updated Successfully!";
                }
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Result = null;
                _responseDto.DisplayMessage = exception.Message;
            }
            return _responseDto;
        }
        #endregion
    }
}
