using Mango.Services.Coupon.AuthAPI.IdentityDataContexts;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.ErrorDtos;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.LoginResponseDto;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.RequestDtos;
using Mango.Services.Coupon.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel;
using Mango.Services.Coupon.AuthAPI.Repository.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Coupon.AuthAPI.Repository.Implementations
{
    public class AuthenticationServiceImplementation : IAuthenticationService
    {
        #region Private Members
        /// <summary>
        /// Identity Data Context for this application
        /// </summary>
        private readonly IdentityDataContext _identityDataContext;

        /// <summary>
        /// User Manager for this application
        /// </summary>
        private readonly UserManager<ApplicationIdentityUserModel> _identityApplicationUserManager;

        /// <summary>
        /// Role Manager for this application
        /// </summary>
        private readonly RoleManager<IdentityRole> _identityRoleManager;

        /// <summary>
        /// Jwt Token Generator Service
        /// </summary>
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identityDataContext"></param>
        public AuthenticationServiceImplementation
        (
            IdentityDataContext identityDataContext,
            UserManager<ApplicationIdentityUserModel> identityApplicationUserManager,
            RoleManager<IdentityRole> identityRoleManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService
        )
        {
            this._identityDataContext = identityDataContext;
            this._identityApplicationUserManager = identityApplicationUserManager;
            this._identityRoleManager = identityRoleManager;
            this._jwtTokenGeneratorService = jwtTokenGeneratorService;
        }
        #endregion

        #region Login For Existing User
        /// <summary>
        /// This method will login user
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            /* To get the valid existing user from database */
            ApplicationIdentityUserModel? existingUser = await this._identityDataContext.TblApplicationIdentityUsers.FirstOrDefaultAsync(user => user.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            /* Check if the valid user exists - If existing user is null then it is not a valid user */
            if (existingUser is null)
            {
                return null;
            }
            else
            {
                /* It will check if the user entered the password for existing user is correct */
                bool isUserEnteredCorrectUserNameAndPassword = await this._identityApplicationUserManager.CheckPasswordAsync(user: existingUser, password: loginRequestDto.Password);

                /* If the user entered the password for existing user is correct */
                if (isUserEnteredCorrectUserNameAndPassword)
                {
                    /* Get roles of existing user */
                    var existingUserRole = await this._identityApplicationUserManager.GetRolesAsync(user: existingUser);

                    /* Generate JWT Token */
                    var token = this._jwtTokenGeneratorService.GenerateToken(applicationUser: existingUser, roles: existingUserRole);

                    /* It is creating Login Response details with user and token */
                    LoginResponseDto loginResponseDto = new()
                    {
                        /* It will return user details and token. Here, 'User' is of type UserDto in LoginResponseDto */
                        User = new UserDto()
                        {
                            Id = existingUser.Id,
                            Name = existingUser.Name,
                            Email = existingUser.Email,
                            PhoneNumber = existingUser.PhoneNumber
                        },

                        /* Initialize Token to return with Login Response when user is valid */
                        Token = token,
                    };

                    /* Return Login Response */
                    return loginResponseDto;
                }
                else
                {
                    return new LoginResponseDto()
                    {
                        User = null,
                        Token = string.Empty
                    };
                }
            }
        }
        #endregion

        #region Register New User Async
        /// <summary>
        /// This method will create new user
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        public async Task<ErrorDto> RegistrationAsync(RegistrationRequestDto registrationRequestDto)
        {
            /* We need to convert registrationRequestDto to ApplicationIdentityUserModel */
            ApplicationIdentityUserModel applicationIdentityUser = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedUserName = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.UserName,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {
                /* It will automatically create a unique user. Duplicate user will not be created based on UserName */
                var createdUserResult = await this._identityApplicationUserManager.CreateAsync(user: applicationIdentityUser, password: registrationRequestDto.Password);

                /* Once new user is created successfully, it will return true */
                if (createdUserResult.Succeeded)
                {
                    /* Get a specific user from the database based on UserName is matched with new user created above */
                    var userToReturn = await this._identityDataContext.TblApplicationIdentityUsers.FirstOrDefaultAsync(user => user.UserName == registrationRequestDto.Email);

                    /* Once new created user is found in database, it will create new User DTO */
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    /* This line will return empty error message - Means, error is not occurred as user is created successfully */
                    return new ErrorDto()
                    {
                        /* It is returning No Error Message during Success */
                        ErrorMessage = string.Empty
                    };
                }
                else
                {
                    /* Returning Error DTO with error message coming from Errors table (From Identity Server Core) */
                    return new ErrorDto()
                    {
                        /* It will return first error */
                        ErrorMessage = createdUserResult.Errors.FirstOrDefault().Description.ToString()
                    };
                }
            }
            catch (Exception exception)
            {
                /* If there is any exception then we return New Error DTO with error message */
                return new ErrorDto()
                {
                    ErrorMessage = exception.Message
                };
            }
        }
        #endregion

        #region Is Role Assigned To User Async
        /// <summary>
        /// Creates a new role for the specified user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IsRoleAssignedToUserAsync(string email, string roleName)
        {
            /* We need to retrieve a user based on the email from the database */
            var existingUser = await this._identityDataContext.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());

            /* We need to check if the user exists. User is not null when user exists */
            if (existingUser != null)
            {
                /* Before we proceed to assign a role to user, we need to check if the role exists (If Role is created in database) */
                if (!this._identityRoleManager.RoleExistsAsync(roleName: roleName).GetAwaiter().GetResult())
                {
                    /* Role does not exist here, hence, we need to create a new role. GetAwaiter().GetResult() is equivalent to await */
                    this._identityRoleManager.CreateAsync(role: new IdentityRole(roleName: roleName)).GetAwaiter().GetResult();
                }

                /* We need to assign the role to the user */
                await this._identityApplicationUserManager.AddToRoleAsync(user: existingUser, role: roleName);

                /* Return true when role is assigned successfully */
                return true;
            }

            /* Return false when role could not assigned for some reason */
            return false;
        }
        #endregion
    }
}
