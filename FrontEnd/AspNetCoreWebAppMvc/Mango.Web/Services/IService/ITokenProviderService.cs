namespace Mango.Web.Services.IService
{
    /// <summary>
    /// This Service is used to store Token information in Cookie
    /// </summary>
    public interface ITokenProviderService
    {
        /// <summary>
        /// Set Token
        /// </summary>
        /// <param name="token"></param>
        void SetTokenInCookie(string token);

        /// <summary>
        /// Get Token
        /// </summary>
        /// <returns></returns>
        string GetTokenFromCookie();

        /// <summary>
        /// Clear Token
        /// </summary>
        void ClearTokenFromCookie();
    }
}
