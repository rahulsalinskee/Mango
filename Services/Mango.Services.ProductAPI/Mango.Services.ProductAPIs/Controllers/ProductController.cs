using Mango.Services.ProductAPIs.DTOs.ProductDtos;
using Mango.Services.ProductAPIs.Repository.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        #region Private Data Members
        /// <summary>
        /// Product service
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// Integer id
        /// </summary>
        private const string INTEGER_ID = "{id:int}";

        /// <summary>
        /// Product Name
        /// </summary>
        private const string STRING_PRODUCT_NAME = "{productName}";

        /// <summary>
        /// Role administrator
        /// </summary>
        private const string ROLE_ADMINISTRATOR = "ADMIN";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        #endregion

        #region Get All Products Async
        /// <summary>
        /// Get products 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await this._productService.GetProductsAsync();

            if (products != null)
            {
                return Ok(products);
            }
            return NotFound();
        }
        #endregion

        #region Get Product By ID Async
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductById/" + INTEGER_ID)]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (ModelState.IsValid)
            {
                var product = await this._productService.GetProductByIdAsync(productId: id);

                if (product != null)
                {
                    return Ok(product);
                }
            }
            return NotFound();
        }
        #endregion

        #region Create Product Async
        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var responseDto = await this._productService.CreateProductAsync(productDto);

            if (ModelState.IsValid)
            {
                if (responseDto != null && responseDto.IsSuccess)
                {
                    return Ok(responseDto);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Update Product By Id Async
        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProductById/" + INTEGER_ID)]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> UpdateProductById(int id, ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                if (productDto is not null)
                {
                    var responseDto = await this._productService.UpdateProductByIdAsync(id: id, productDto: productDto);
                    if (responseDto != null && responseDto.IsSuccess)
                    {
                        return Ok(responseDto);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Update Product By Product Name
        /// <summary>
        /// Update product by product name
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProductByName/{productName}")]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> UpdateProductByName(string productName, [FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                if (productDto is not null)
                {
                    var responseDto = await this._productService.UpdateProductByNameAsync(productName: productName, productDto: productDto);

                    if (responseDto != null && responseDto.IsSuccess)
                    {
                        return Ok(responseDto);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete Product By ID Async
        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteProductById/" + INTEGER_ID)]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            if (id > 0 && ModelState.IsValid)
            {
                var responseDto = await this._productService.DeleteProductByIdAsync(id);

                if (responseDto.Result != null && responseDto.IsSuccess)
                {
                    return Ok(responseDto);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete Product By Product Name Async
        /// <summary>
        /// Delete product by name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteProductByName/" + STRING_PRODUCT_NAME)]
        [Authorize(Roles = ROLE_ADMINISTRATOR)]
        public async Task<IActionResult> DeleteProductByName(string productName)
        {
            if (!string.IsNullOrEmpty(productName) && ModelState.IsValid)
            {
                var responseDto = await this._productService.DeleteProductByNameAsync(productName: productName);

                if (responseDto != null && responseDto.IsSuccess)
                {
                    return Ok(responseDto);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
