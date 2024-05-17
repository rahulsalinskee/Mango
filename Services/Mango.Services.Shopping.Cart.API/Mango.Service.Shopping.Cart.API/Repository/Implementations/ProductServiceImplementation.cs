using Mango.Service.Shopping.Cart.API.DTOs.Product;
using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.Repository.Services;
using Newtonsoft.Json;

namespace Mango.Service.Shopping.Cart.API.Repository.Implementations
{
    public class ProductServiceImplementation : IProductService
    {
        /// <summary>
        /// Http Client
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Product API
        /// </summary>
        private const string PRODUCT_API_END_POINT = $"/api/product/GetAllProducts";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public ProductServiceImplementation(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Consume Product API to get all products
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            /* 
            *  Create Http Client to consume Product API
            *  This name parameter should match with HttpClient name in Program.cs
            */
            var client = this._httpClientFactory.CreateClient(name: "Product");

            /* To get the API response */
            var response = await client.GetAsync(requestUri: PRODUCT_API_END_POINT);

            /* To get the API content from the response */
            var apiContent = await response.Content.ReadAsStringAsync();

            /* To convert the API content to Response DTO */
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(value: apiContent);

            /* 
            *  IF - responseDto is successful 
            *  THEN - Convert response to String 
            *  and then De-serialize the response DTO to the List of Product DTO 
            */
            if (responseDto.IsSuccess)
            {
                /* Convert the response to string and then de-serialize it (response DTO) to List of Product DTO */
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(value: responseDto.Result.ToString());
            }

            /* 
            *  IF - responseDto is not successful 
            *  THEN - Return a new and empty List of Product DTO
            */
            return new List<ProductDto>();
        }
    }
}
