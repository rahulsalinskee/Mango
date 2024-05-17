using Mango.Web.Models.AuthenticationModels.DTOs.RequestDto;
using Mango.Web.Models.CommonDTOs;

namespace Mango.Web.Services.IService
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Login Async
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto>? LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Register Async
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto>? RegisterAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Assign Role
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto>? AssignRoleAsync(RegistrationRequestDto registrationRequestDto);


    }
}
