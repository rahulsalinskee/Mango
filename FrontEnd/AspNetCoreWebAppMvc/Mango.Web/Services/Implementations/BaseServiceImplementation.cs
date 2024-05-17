using Mango.Web.Models.CommonDTOs;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services.Implementations
{
    public class BaseServiceImplementation : IBaseService
    {
        #region Private  Data Members
        /// <summary>
        /// HttpClientFactory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// TokenProvider
        /// </summary>
        private readonly ITokenProviderService _tokenProviderService;

        /// <summary>
        /// Application JSON
        /// </summary>
        private const string APPLICATION_JSON = "application/json";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public BaseServiceImplementation(IHttpClientFactory httpClientFactory, ITokenProviderService tokenProviderService)
        {
            this._httpClientFactory = httpClientFactory;
            this._tokenProviderService = tokenProviderService;
        }
        #endregion

        #region Send Request Async
        /// <summary>
        /// This method will be used to send HTTP Request message and return response message
        /// By default we are setting it as true. But we do not need to pass the token when a user is registered or they are logging in.
        /// We need to go to the caller method place and set this value as false.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <param name="isBearerTokenIncluded"></param>
        /// <returns></returns>
        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool isBearerTokenIncluded = true)
        {
            try
            {
                /* HttpClient is created using IHttpClientFactory for making API calls */
                HttpClient httpClient = this._httpClientFactory.CreateClient(name: "MangoAPI");

                /* While making API calls, we will be using HTTP Request Message & provide configure options on that message */
                HttpRequestMessage httpRequestMessage = new();

                /* This is to specify what kind of message we are accepting. Here we are accepting application/JSON */
                httpRequestMessage.Headers.Add(name: "Accept", value: APPLICATION_JSON);

                /* TOKEN CODE - Authentication Token will be passed here */
                if (isBearerTokenIncluded)
                {
                    /* Setting the Authorization header with Bearer token. */
                    var token = this._tokenProviderService.GetTokenFromCookie();

                    /* Setting the Authorization header with Bearer token. */
                    httpRequestMessage.Headers.Add(name: "Authorization", value: $"Bearer {token}");
                }

                /* This is to specify the URL that we will invoke to access any API. */
                httpRequestMessage.RequestUri = new Uri(uriString: requestDto.Url);

                /* 
                * We also need to serialize requestDto data for Post or Put request (Not for Get & Delete). 
                * If requestDto Data is Null, that means, requestDto APi type is either GET or DELETE request.
                * Hence checking if the data is not null 
                */
                if (requestDto.Data != null)
                {
                    /* We also need to serialize data for Post or Put request (Not for Get & Delete). Hence serializing requestDto data here */
                    httpRequestMessage.Content = new StringContent(content: JsonConvert.SerializeObject(value: requestDto.Data), encoding: Encoding.UTF8, mediaType: APPLICATION_JSON);
                }

                /* This is HTTP Response Message that we will be getting while sending HTTP Request message. */
                HttpResponseMessage? apiResponseMessage = null;

                /* Based on API type, we need to configure httpRequestMessage method */
                switch (requestDto.ApiType)
                {
                    case StaticDetails.ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;

                    case StaticDetails.ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;

                    case StaticDetails.ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;

                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                /* Sending HTTP Request message and getting response message */
                apiResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);

                /* Check the status code of the response we received */
                switch (apiResponseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            DisplayMessage = "Api Not Found!"
                        };

                    case HttpStatusCode.Forbidden:
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            DisplayMessage = "Access Denied!"
                        };

                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            DisplayMessage = "Unauthorized!"
                        };

                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            DisplayMessage = "Internal Server Error!"
                        };

                    default:
                        /* Here, Default case is Success (With Status Code OK). Hence, we will be De-serializing response content */
                        var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();

                        /* De-serializing response content */
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(value: apiContent);

                        /* Returning the De-serialized response */
                        return apiResponseDto;
                }
            }
            catch (Exception exception)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    DisplayMessage = exception.Message
                };
            }
        }
        #endregion
    }
}
