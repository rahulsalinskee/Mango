using static Mango.Web.Utilities.StaticDetails;

namespace Mango.Web.Models.CommonDTOs
{
    public class RequestDto
    {
        /// <summary>
        /// Api type GET or POST request
        /// </summary>
        public ApiType ApiType { get; set; } = ApiType.GET;

        /// <summary>
        /// Url to call
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Data to send with request
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Access token to send with request
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
    }
}
