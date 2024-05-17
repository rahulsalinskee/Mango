using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.ProductAPIs.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        #region Add Web Application Authentication Extension
        /// <summary>
        /// Add WebApplication Authentication Extensions
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddWebApplicationAuthenticationExtensions(this WebApplicationBuilder builder)
        {
            /* Getting values of ApiSettings section from ApiSettings */
            var apiSettingSection = builder.Configuration.GetSection(key: "ApiSettings");

            /* Getting secret signing key from the values from ApiSettings */
            var secretSigninKey = apiSettingSection.GetValue<string>(key: "SigninKey");

            /* Getting issuer from the values from ApiSettings */
            var issuer = apiSettingSection.GetValue<string>(key: "Issuer");

            /* Getting audience key from the values from ApiSettings */
            var audience = apiSettingSection.GetValue<string>(key: "Audience");

            /* Encoding Secret Signing Key */
            var encodedSecretSigninKey = Encoding.ASCII.GetBytes(secretSigninKey);

            /* Add Authentication */
            builder.Services.AddAuthentication(option =>
            {
                /* Configure Options */
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                /* Parameters validation for validating of JWT token */
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    /* We need define what do we want to validate */
                    IssuerSigningKey = new SymmetricSecurityKey(key: encodedSecretSigninKey),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            /* Return builder */
            return builder;
        } 
        #endregion
    }
}
