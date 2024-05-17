using Mango.Web.Services.IService;
using Mango.Web.Utilities;

namespace Mango.Web.Services.Implementations
{
    public class TokenProviderServiceImplementation : ITokenProviderService
    {
        #region Private Data Members
        /// <summary>
        /// Whenever we are working with Cookie then we need to use HttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for TokenProvider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public TokenProviderServiceImplementation(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Clear Token From Cookie
        /// <summary>
        /// Clear Token
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ClearTokenFromCookie()
        {
            /* Removing Token from Cookie using Key which is used while setting Token in Cookie */
            this._httpContextAccessor.HttpContext?.Response.Cookies.Delete(key: StaticDetails.TOKEN_COOKIE);
        }
        #endregion

        #region Get Token From Cookie
        /// <summary>
        /// Get Token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string? GetTokenFromCookie()
        {
            string? token = null;

            /* Get Token from Cookie using Key in TryGetValue which returns true if (out) Token has value else returns false */
            bool? hasToken = this._httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(key: StaticDetails.TOKEN_COOKIE, value: out token);

            return hasToken is true ? token : null;
        }
        #endregion

        #region Set Token In Cookie
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetTokenInCookie(string token)
        {
            /* Set Token in Cookie using Key/Value pair */
            this._httpContextAccessor.HttpContext?.Response.Cookies.Append(key: StaticDetails.TOKEN_COOKIE, value: token);
        } 
        #endregion
    }
}
