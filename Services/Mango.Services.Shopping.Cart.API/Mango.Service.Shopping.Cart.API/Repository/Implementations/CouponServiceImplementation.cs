using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.DTOs.Coupon;
using Mango.Service.Shopping.Cart.API.Repository.Services;
using Newtonsoft.Json;

namespace Mango.Service.Shopping.Cart.API.Repository.Implementations
{
    public class CouponServiceImplementation : ICouponService
    {
        #region Private Data Members
        /// <summary>
        /// Http Client Factory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Coupon API End Point
        /// </summary>
        //private const string COUPON_API_END_POINT = $"/api/coupon/";
        private const string COUPON_API_END_POINT = "/api/coupon/GetCouponByCode/";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public CouponServiceImplementation(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Get Coupon By Code Async
        /// <summary>
        /// Get Coupon By Code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
        {
            /* 
            *  Create Http Client to consume Coupon API
            *  This name parameter should match with HttpClient name in Program.cs
            */
            var client = this._httpClientFactory.CreateClient(name: "Coupon");

            /* To get the API response */
            var httpResponse = await client.GetAsync(requestUri: $"/api/coupon/GetCouponByCode/{couponCode}");

            /* To get the API content from the response */
            var apiContent = await httpResponse.Content.ReadAsStringAsync();

            /* To convert the API content to Response DTO */
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(value: apiContent);

            /* 
            *  IF - responseDto is successful 
            *  THEN - Convert response to String 
            *  and then De-serialize the response DTO to the List of Product DTO 
            */
            if (responseDto.IsSuccess)
            {
                /* Convert the response Dto to string and then de-serialize it (response DTO) to get Coupon DTO */
                return JsonConvert.DeserializeObject<CouponDto>(value: responseDto.Result.ToString());
            }

            /* 
            *  IF - responseDto is not successful 
            *  THEN - Return a new and empty List of Product DTO
            */
            return new CouponDto();
        } 
        #endregion
    }
}
