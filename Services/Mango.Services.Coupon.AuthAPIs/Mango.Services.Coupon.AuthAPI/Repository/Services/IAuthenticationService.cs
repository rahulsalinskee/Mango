using Mango.Services.Coupon.AuthAPI.Models.DTOs;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.ErrorDtos;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.LoginResponseDto;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.RequestDtos;

namespace Mango.Services.Coupon.AuthAPI.Repository.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Register A New User
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        public Task<ErrorDto> RegistrationAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Login the existing user
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Authenticates the specified user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<bool> IsRoleAssignedToUserAsync(string email, string roleName);
    }
}
