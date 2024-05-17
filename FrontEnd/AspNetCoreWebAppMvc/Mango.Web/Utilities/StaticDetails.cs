namespace Mango.Web.Utilities
{
    public class StaticDetails
    {
        /// <summary>
        /// API Type (GET, POST, PUT, DELETE)
        /// </summary>
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }

        /// <summary>
        /// Coupon API Base URL
        /// </summary>
        public static string? CouponAPIBaseURL { get; set; }

        /// <summary>
        /// Auth API Base URL
        /// </summary>
        public static string? AuthAPIBaseURL { get; set; }

        /// <summary>
        /// Product API Base URL
        /// </summary>
        public static string? ProductAPIBaseURL { get; set; } // ProductAPIBaseURL

        /// <summary>
        /// Admin Role Base URL
        /// </summary>
        public const string ROLE_ADMINISTRATOR = "ADMIN";

        /// <summary>
        /// Customer Role Base URL
        /// </summary>
        public const string ROLE_CUSTOMER = "CUSTOMER";

        /// <summary>
        /// Set and Get Token In/From Cookie
        /// </summary>
        public const string TOKEN_COOKIE = "JWTToken";
    }
}
