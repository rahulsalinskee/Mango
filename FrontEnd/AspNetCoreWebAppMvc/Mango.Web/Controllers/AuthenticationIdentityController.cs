using Mango.Web.Models.AuthenticationModels.DTOs.RequestDto;
using Mango.Web.Models.AuthenticationModels.DTOs.ResponseDto;
using Mango.Web.Models.CommonDTOs;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthenticationIdentityController : Controller
    {
        #region Private Data Members
        /// <summary>
        /// Instance of IAuthenticationService
        /// </summary>
        private readonly Mango.Web.Services.IService.IAuthenticationService _authenticationService;

        /// <summary>
        /// Instance of ITokenProviderService
        /// </summary>
        private readonly ITokenProviderService _tokenProviderService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the AuthenticationIdentityController
        /// </summary>
        /// <param name="authenticationService"></param>
        public AuthenticationIdentityController(Mango.Web.Services.IService.IAuthenticationService authenticationService, ITokenProviderService tokenProviderService)
        {
            this._authenticationService = authenticationService;
            this._tokenProviderService = tokenProviderService;
        }
        #endregion

        #region Login Async - GET & POST
        /// <summary>
        /// Login Async - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        /// <summary>
        /// Login Async - POST
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDtoData)
        {
            ResponseDto loginResultResponse = await this._authenticationService.LoginAsync(loginRequestDto: loginRequestDtoData);

            /* loginResultResponse is not null and loginResultResponse.IsSuccess is true, that means login is successful */
            if (loginResultResponse != null && loginResultResponse.IsSuccess)
            {
                /* Need to De-Serialize the loginResultResponse.Result to get LoginResponseDto */
                LoginResponseDto? loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(loginResultResponse.Result));

                /* Sign-In the user using the build-in Identity framework */
                await this.SignInUserAsync(loginResponseDto: loginResponseDto);

                /* Set Token (Which is coming from LoginResponseDto) in Cookie, which means user is logged in */
                this._tokenProviderService.SetTokenInCookie(token: loginResponseDto.Token);

                /* Redirect To Index action method of Home Controller */
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            else
            {
                /* Display Error Message in Register View through Toast message */
                TempData["ErrorMessage"] = loginResultResponse?.DisplayMessage;

                /* Return To The View With The login request data passed */
                return View(loginRequestDtoData);
            }
        }
        #endregion

        #region Register Async - GET & POST
        /// <summary>
        /// Register Async - GET 
        /// It will load the UI with the roles options
        /// GET - [Data To UI From Server (UI <== Controller <== Action Method <== Service <== Database)]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            /* To Show the roles in drop down list items in register view */
            IEnumerable<SelectListItem> roles = PopulateDropDownWithRoleInUi();

            /* To pass the roles to the view using ViewBag */
            ViewBag.Roles = roles;

            RegistrationRequestDto registrationRequestDto = new();
            return View(registrationRequestDto);
        }

        /// <summary>
        /// Register Async - POST 
        /// It will send the user's input data to server
        /// POST - [Data From UI to Server (UI ==> Controller ==> Action Method ==> Service ==> Database)]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto userRegistrationRequestDtoData)
        {
            ResponseDto? registrationResultResponse = await this._authenticationService.RegisterAsync(registrationRequestDto: userRegistrationRequestDtoData);

            ResponseDto? assignRole;

            /* registrationResultResponse is not null and registrationResultResponse.IsSuccess is true, that means Registration is successful */
            if (registrationResultResponse != null && registrationResultResponse.IsSuccess)
            {
                /* If user has not provided role in registration data, then assign the default role as customer */
                if (string.IsNullOrEmpty(userRegistrationRequestDtoData.Role))
                {
                    /* Setting Customer Role as default (When user has not selected role while registration) */
                    userRegistrationRequestDtoData.Role = StaticDetails.ROLE_CUSTOMER;
                }

                /* If user has provided role in registration data, then create a role selected from drop down list in UI */
                assignRole = await this._authenticationService.AssignRoleAsync(registrationRequestDto: userRegistrationRequestDtoData);

                /* If assign role is not null and role is assigned successfully then redirect from registration page to Login page */
                if (assignRole != null && assignRole.IsSuccess)
                {
                    /* Sending Registration Success Message Register Action Method to Register View using Temp Data */
                    TempData["SuccessMessage"] = "Registration is successful!";

                    /* Redirecting To Login Page After Successful Registration. Don't use nameof() as it will not work nameof(AuthenticationIdentityController) due to "Controller" as suffix */
                    return RedirectToAction(actionName: "Login", controllerName: "AuthenticationIdentity");
                }
            }
            else
            {
                /* Display Error Message in Register View through Toast message */
                TempData["ErrorMessage"] = registrationResultResponse?.DisplayMessage;
            }

            /* To Show the roles in drop down list items in register view */
            IEnumerable<SelectListItem> roles = PopulateDropDownWithRoleInUi();

            /* To pass the roles to the view using ViewBag */
            ViewBag.Roles = roles;
            return View(userRegistrationRequestDtoData);
        }
        #endregion

        #region Logout
        /// <summary>
        /// Logout Async - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            /* This will do Sign-Out */
            await this.HttpContext.SignOutAsync();

            /* Clear Token (Which is coming from LoginResponseDto) in Cookie, which means user is logged out */
            this._tokenProviderService.ClearTokenFromCookie();

            /* Redirect To Index action method of Home Controller */
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        #endregion

        #region Private static Method - PopulateDropDownWithRoleInUi
        /// <summary>
        /// To Show the roles in drop down list items in register view
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> PopulateDropDownWithRoleInUi()
        {
            /* To Show the roles in drop down list items in register view */
            IEnumerable<SelectListItem> roles = new List<SelectListItem>()
            {
                /* Adding a Select List Item For Customer */
                new SelectListItem()
                {
                    Text = StaticDetails.ROLE_CUSTOMER,
                    Value = StaticDetails.ROLE_CUSTOMER
                },

                /* Adding a Select List Item For Administrator */
                new SelectListItem()
                {
                    Text = StaticDetails.ROLE_ADMINISTRATOR,
                    Value = StaticDetails.ROLE_ADMINISTRATOR
                },
            };
            return roles;
        }

        /// <summary>
        /// 
        /// To build claim principal
        /// These are basic and default steps which is needed to Sign-In the user using the build-in Identity framework
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        private async Task SignInUserAsync(LoginResponseDto loginResponseDto)
        {
            /* Create A New Handler */
            JwtSecurityTokenHandler handler = new();

            /* Decoding the TOKEN from Login Response - On Handler there is method - ReadJwtToken */
            var jwtToken = handler.ReadJwtToken(token: loginResponseDto.Token);

            /* Need to extract all the claims from JWT Token coming from Login Response to create claim principal */
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            /* Add claim for Email - Here, we are extracting the claim For Email, of JWT Token coming from Login Response (This is the same claim of JWT Token Generator implementation) */
            identity.AddClaim(claim: new Claim(type: JwtRegisteredClaimNames.Email, value: jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email).Value));

            /* Add claim for Subject - Here, we are extracting the claim For Subject, of JWT Token coming from Login Response (This is the same claim of JWT Token Generator implementation) */
            identity.AddClaim(claim: new Claim(type: JwtRegisteredClaimNames.Sub, value: jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value));

            /* Add claim for Name - Here, we are extracting the claim For Name, of JWT Token coming from Login Response (This is the same claim of JWT Token Generator implementation) */
            identity.AddClaim(claim: new Claim(type: JwtRegisteredClaimNames.Name, value: jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name).Value));

            /* 
            * When we work with .NET IDENTITY, we need to extract two more claims (Name - Email And Role) from JWT Token which is coming from Login Response
            * We need to add claims using ClaimType for Name & Email
            * Add claim for Name - Here, we are extracting the claim For Email, of JWT Token coming from Login Response
            */
            identity.AddClaim(claim: new Claim(type: ClaimTypes.Name, value: jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email).Value));

            /* 
            * Add claim for Role - Here, we are extracting the claim For Role, of JWT Token coming from Login Response 
            * The reason of using "ClaimTypes.Role" is, because with .NET integration if we have added "ClaimType.Role" then in controller, if we add
            * authorize attribute (on the top of any action method) and initialize the Role with "ADMIN", then it will be automatically taken care of,
            * Because claim is assigned when we are signing in the user inside HttpContext.SignInAsync() method.
            * That is why, it is critical to add this line of code
            */
            identity.AddClaim(claim: new Claim(type: ClaimTypes.Role, value: jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value));

            /* To build claim principal by passing identity in the constructor of ClaimsPrincipal */
            ClaimsPrincipal claimsPrincipal = new(identity: identity);

            /* We need to call SignInAsync method to build claim principal by passing AuthenticationScheme and ClaimsPrincipal */
            await HttpContext.SignInAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme, principal: claimsPrincipal);
        }
        #endregion
    }
}
