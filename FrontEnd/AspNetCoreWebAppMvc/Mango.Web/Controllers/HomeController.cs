using Mango.Web.Models;
using Mango.Web.Models.CommonDTOs;
using Mango.Web.Models.ProductModels.DTOs;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
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
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public HomeController(IProductService productService)
        {
            this._productService = productService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
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

        #region Product Details
        /// <summary>
        /// Product Details By Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            ResponseDto? responseDto = await this._productService.GetProductByIdAsync(productId: productId);

            if (ModelState.IsValid)
            {
                if (responseDto?.Result is not null && responseDto.IsSuccess)
                {
                    var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto?.Result));
                    return View(productDto);
                }
                else
                {
                    TempData["ErrorMessage"] = responseDto?.DisplayMessage;
                }
            }
            return View(responseDto);
        } 
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
