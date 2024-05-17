using Mango.Services.Coupon.AuthAPI.Models.DTOs.CommonResponseDtos;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.ErrorDtos;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.RequestDtos;
using Mango.Services.Coupon.AuthAPI.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.Coupon.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        #region Private Members
        private readonly IAuthenticationService _authenticationService;
        protected ResponseDto _responseDto; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authenticationService"></param>
        public IdentityController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
            _responseDto = new ResponseDto();
        } 
        #endregion

        #region Register Async
        /// <summary>
        /// RegisterAsync
        /// POST [Data From UI to Server(Controller ==> Action Method ==> Service ==> Database)]
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            /* If ErrorDto is empty, it means there is no error while registering a new user */
            ErrorDto errorDto = await this._authenticationService.RegistrationAsync(registrationRequestDto: registrationRequestDto);

            /* If ErrorDto is not null or empty, it means there is an error occurred while registering a new user */
            if (!string.IsNullOrEmpty(errorDto.ErrorMessage))
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = errorDto.ErrorMessage;
                return BadRequest(this._responseDto);
            }
            else
            {
                this._responseDto.IsSuccess = true;
                return Ok(this._responseDto);
            }
        }
        #endregion

        #region Login Async
        /// <summary>
        /// LoginAsync
        /// POST [Data From UI to Server(Controller ==> Action Method ==> Service ==> Database)]
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto? loginRequestDto)
        {
            var loginResponse = await this._authenticationService.LoginAsync(loginRequestDto: loginRequestDto);

            if (!string.IsNullOrEmpty(loginResponse?.Token))
            {
                this._responseDto.Result = loginResponse;
                this._responseDto.IsSuccess = true;
                this._responseDto.DisplayMessage = "Login Successful!";
                return Ok(this._responseDto);
            }
            else
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = "Login Failed";
                return BadRequest(this._responseDto);
            }
        } 
        #endregion

        #region Assign Role Async
        /// <summary>
        /// Assign Role
        /// POST [Data From UI to Server(Controller ==> Action Method ==> Service ==> Database)]
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRoleAsync([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            bool isRoleAssigned = await this._authenticationService.IsRoleAssignedToUserAsync(email: registrationRequestDto.Email, roleName: registrationRequestDto.Role.ToUpper());

            if (!isRoleAssigned)
            {
                this._responseDto.DisplayMessage = "Error Encountered While Assigning Role!";
                this._responseDto.IsSuccess = false;
                return BadRequest(this._responseDto);
            }
            else
            {
                this._responseDto.DisplayMessage = "Role Assigned Successfully!";
                this._responseDto.IsSuccess = true;
                return Ok(this._responseDto);
            }
        } 
        #endregion
    }
}
