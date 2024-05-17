using Mango.Web.Models.AuthenticationModels.DTOs.RequestDto;
using Mango.Web.Models.CommonDTOs;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;

namespace Mango.Web.Services.Implementations
{
    public class AuthenticationServiceImplementation : IAuthenticationService
    {
        #region Private Data Members
        /// <summary>
        /// Base Service
        /// </summary>
        private readonly IBaseService _baseService; 

        /// <summary>
        /// Identity Authentication Controller API End Point
        /// </summary>
        private const string IDENTITY_AUTHENTICATION_CONTROLLER_API_END_POINT = "/api/Identity";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseService"></param>
        public AuthenticationServiceImplementation(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        #endregion

        #region Assign Role Async
        /// <summary>
        /// Assign Role Async
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto>? AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await this._baseService.SendAsync(requestDto: new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetails.AuthAPIBaseURL + IDENTITY_AUTHENTICATION_CONTROLLER_API_END_POINT + "/AssignRole",
            });
        }
        #endregion

        #region Login Async
        /// <summary>
        /// Login Async
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto>? LoginAsync(LoginRequestDto loginRequestDto)
        {
            /* Send Async excepts 2 arguments - requestDto and isBearerTokenIncluded: This will be false while user is Registering, Login and AssignRole */
            return await this._baseService.SendAsync(requestDto: new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticDetails.AuthAPIBaseURL + IDENTITY_AUTHENTICATION_CONTROLLER_API_END_POINT + "/login",
            }, isBearerTokenIncluded: false);
        }
        #endregion

        #region Register Async
        /// <summary>
        /// Register Async
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns>It returns ResponseDto</returns>
        /// <exception cref="NotImplementedException"></returns>
        public async Task<ResponseDto>? RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            /* Send Async excepts 2 arguments - requestDto and isBearerTokenIncluded: This will be false while user is Registering, Login and AssignRole */
            return await this._baseService.SendAsync(requestDto: new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetails.AuthAPIBaseURL + IDENTITY_AUTHENTICATION_CONTROLLER_API_END_POINT + "/register",
            }, isBearerTokenIncluded: false);
        } 
        #endregion
    }
}
