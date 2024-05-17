using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.ProductModels.DTOs;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        #region Private Data Members
        /// <summary>
        /// Product Service
        /// </summary>
        private readonly IProductService _productService;
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
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            IEnumerable<ProductDto>? products = new List<ProductDto>();
            ResponseDto? productsResponse = await this._productService.GetProductsAsync();

            if (ModelState.IsValid)
            {
                if (productsResponse?.Result is not null && productsResponse.IsSuccess)
                {
                    TempData["SuccessMessage"] = $"{products.Count()} Number of products fetched!";
                    products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(productsResponse?.Result));
                }
                else
                {
                    TempData["ErrorMessage"] = productsResponse?.DisplayMessage;
                }
            }
            return View(products);
        }
        #endregion

        #region Get Product By Id Async
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? productByIdResponse = await this._productService.GetProductByIdAsync(productId: id);
                if (productByIdResponse?.Result is not null && productByIdResponse.IsSuccess)
                {
                    var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productByIdResponse?.Result));
                    TempData["SuccessMessage"] = "Product Fetched Successfully!";
                    return RedirectToAction(actionName: nameof(GetAllProducts));
                }
                else
                {
                    TempData["ErrorMessage"] = productByIdResponse?.DisplayMessage;
                }
                return NotFound();
            }
            return NotFound();
        }
        #endregion
         
        #region Get Product By Name Async
        /// <summary>
        /// Get Product By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetProductByName(string name)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? productByNameResponse = await this._productService.GetProductByNameAsync(productName: name);

                if (productByNameResponse?.Result is not null && productByNameResponse.IsSuccess)
                {
                    var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productByNameResponse?.Result));
                    TempData["SuccessMessage"] = "Product Fetched Successfully!";
                    return View(productDto);
                }
                else
                {
                    TempData["ErrorMessage"] = productByNameResponse?.DisplayMessage;
                }
                return BadRequest();
            }
            return BadRequest();
        }
        #endregion

        #region Create Product Async - GET | POST
        /// <summary>
        /// Create product To Load the UI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductCreate()
        {
            return View();
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (productDto is not null)
                    {
                        var response = await this._productService.CreateProductAsync(productDto: productDto);
                        if (response?.Result is not null && response.IsSuccess)
                        {
                            TempData["SuccessMessage"] = "Product Created Successfully!";
                            return RedirectToAction(actionName: nameof(GetAllProducts));
                        }
                    }
                }
                return BadRequest();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Update Product Async By Id - GET | POST
        /// <summary>
        /// Update product by ID - GET
        /// Get the product details by id on Update product view
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> UpdateProductById(int productId)
        {
            ResponseDto? responseDto = await this._productService.GetProductByIdAsync(productId: productId);

            if (responseDto?.Result is not null && responseDto.IsSuccess)
            {
                ProductDto? productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto?.Result));
                return View(productDto);
            }
            else
            {
                TempData["ErrorMessage"] = responseDto?.DisplayMessage;
            }
            return NotFound();
        }

        /// <summary>
        /// Update product by id - POST
        /// Update the product fetched by id on Update product view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProductById(ProductDto productDto)
        {
            ResponseDto? productResponseDto = await this._productService.UpdateProductByIdAsync(productId: productDto.ProductId, productDto: productDto);
            if (productResponseDto?.Result is not null && productResponseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Product Updated Successfully!";
                return RedirectToAction(actionName: nameof(ProductController.GetAllProducts));
            }
            else
            {
                TempData["ErrorMessage"] = productResponseDto?.DisplayMessage;
                return View(productDto);
            }
        }
        #endregion

        #region Update Product Async By Name - GET | POST
        /// <summary>
        /// Update product by Name - GET
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> UpdateProductByName(string productName)
        {
            ResponseDto? responseDto = await this._productService.GetProductByNameAsync(productName: productName);

            if (responseDto?.Result is not null && responseDto.IsSuccess)
            {
                ProductDto? productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto?.Result));
                return View(productDto);
            }
            else
            {
                TempData["ErrorMessage"] = responseDto?.DisplayMessage;
            }
            return NotFound();
        }

        /// <summary>
        /// Update product by Name - POST
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProductByName(ProductDto productDto)
        {
            ResponseDto? productResponseDto = await this._productService.UpdateProductByNameAsync(productName: productDto.Name, productDto: productDto);
            if (productResponseDto?.Result is not null && productResponseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Product Updated Successfully!";
                return RedirectToAction(nameof(ProductController.GetAllProducts));
            }
            else
            {
                TempData["ErrorMessage"] = productResponseDto?.DisplayMessage;
                return View(productDto);
            }
        }
        #endregion

        #region Delete Product By product ID Async
        /// <summary>
        /// Delete product by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteProductById(int productId)
        {
            ResponseDto? couponResponseDto = await this._productService.GetProductByIdAsync(productId: productId);

            if (couponResponseDto?.Result is not null && couponResponseDto.IsSuccess)
            {
                var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(couponResponseDto?.Result));
                TempData["SuccessMessage"] = "Coupon Fetched Successfully!";
                return View(productDto);
            }
            else
            {
                TempData["ErrorMessage"] = couponResponseDto?.DisplayMessage;
            }
            return NotFound();
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteProductById(ProductDto productDto)
        {
            ResponseDto? couponResponseDto = await this._productService.DeleteProductByIdAsync(productId: productDto.ProductId);

            if (couponResponseDto?.Result is not null && couponResponseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Coupons Deleted Successfully!";
                //return View(productDto);
                return RedirectToAction(nameof(GetAllProducts));
            }
            else
            {
                TempData["ErrorMessage"] = couponResponseDto?.DisplayMessage;
            }
            return NotFound();
        }
        #endregion

        #region Delete Product By Name Async
        /// <summary>
        /// Delete product by Name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProductByName(string name, ProductDto productDto)
        {
            ResponseDto? productResponseDto = await this._productService.GetProductByNameAsync(productName: name);

            if (productResponseDto?.Result is not null && productResponseDto.IsSuccess)
            {
                TempData["SuccessMessage"] = "Coupons Deleted Successfully!";
                return RedirectToAction(nameof(GetAllProducts));
            }
            else
            {
                TempData["ErrorMessage"] = productResponseDto?.DisplayMessage;
            }
            return View(productDto);
        }
        #endregion
    }
}
