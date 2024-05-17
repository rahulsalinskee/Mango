using Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel;
using Mango.Services.Coupon.AuthAPI.Models.JwtModel;
using Mango.Services.Coupon.AuthAPI.Repository.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.Coupon.AuthAPI.Repository.Implementations
{
    /// <summary>
    /// This class will generate token and will be consumed by LoginAsync method of Authentication Service Implementation class
    /// </summary>
    public class JwtTokenGeneratorServiceImplementation : IJwtTokenGeneratorService
    {
        #region Private Data Members
        /// <summary>
        /// Private Data Members
        /// </summary>
        private readonly JWTOptions _jwtOptions; 
        #endregion

        #region Constructor
        /// <summary>
        /// Here, We can't inject JWTOptions normally as it is not a service and there is no class implementation that we register in Program.cs
        /// Since, we have configured JWTOptions with help of builder.services.configure with the options.
        /// Hence, we are passing I JWTOptions as parameters to the constructor
        /// </summary>
        /// <param name="jwtOptions"></param>
        public JwtTokenGeneratorServiceImplementation(IOptions<JWTOptions> jwtOptions)
        {
            this._jwtOptions = jwtOptions.Value;
        }
        #endregion

        #region Generate JWT Token
        /// <summary>
        /// This method will generate token based on the application user. 
        /// There are Four required steps to generate JWT token.
        /// 1. Secrete Key (Key). 
        /// 2. Claim List (Claims) with roles.
        /// 3. Create Token Descriptor
        /// 4. Create token.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GenerateToken(ApplicationIdentityUserModel applicationUser, IEnumerable<string> roles)
        {
            /* Create an instance of JwtSecurityTokenHandler. It has a Create Token method. */
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            /* 1. Extract Secrete Key (SigninKey) and Encode it ASCII from AppSettings */
            var secreteSigninKey = Encoding.ASCII.GetBytes(this._jwtOptions.SigninKey);

            /* 2. Inside TOKEN, we typically have a set of claims. It is a collection of key/value pairs. It is used to store the values in a secure way. We can add multiple claims. */
            var claims = new List<Claim>()
            {
                /* Here, we are adding a new claim with key as Email and value as Email. Here, Email is typically used to store the user's Email */
                new(JwtRegisteredClaimNames.Email, applicationUser.Email),

                /* Here, we are adding a new claim with key as Subject and value as ID. Here, Subject (Sub) is typically used to store the user's ID */
                new(JwtRegisteredClaimNames.Sub, applicationUser.Id),

                /* Here, we are adding a new claim with key as Name and value as Name. Here, Name is typically used to store the user's userName */
                new(JwtRegisteredClaimNames.Name, applicationUser.UserName),
            };

            /* Here, we are adding a new claim with key as Roles and value as Roles. Here, Roles are typically used to store the user's role(s) */
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            /* 3. Create TOKEN Descriptor with all the properties we want - It is used to define the properties of the token. */
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                /* Setting the Audience From AppSettings using JWTOptions */
                Audience = this._jwtOptions.Audience,

                /* Setting the Issuer From AppSettings using JWTOptions */
                Issuer = this._jwtOptions.Issuer,

                /* Setting the Subject as Claims */
                Subject = new ClaimsIdentity(claims),

                /* Setting the Expiration Date/Time. Here, it is 15 minutes. */
                Expires = DateTime.UtcNow.AddMinutes(15),

                /* Setting the Signing Key from step first. HmacSha256Signature is the government standard security algorithm */
                SigningCredentials = new SigningCredentials(key: new SymmetricSecurityKey(key: secreteSigninKey), algorithm: SecurityAlgorithms.HmacSha256Signature),
            };

            /* 4. Create Token with help of JwtSecurityTokenHandler and passing TOKEN Descriptor inside */
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            /* 5. Return final Token */
            return jwtSecurityTokenHandler.WriteToken(token: token);
        } 
        #endregion
    }
}
