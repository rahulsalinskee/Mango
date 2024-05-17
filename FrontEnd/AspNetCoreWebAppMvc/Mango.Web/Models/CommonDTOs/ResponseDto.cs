namespace Mango.Web.Models.CommonDTOs
{
    public class ResponseDto
    {
        /// <summary>
        /// The result of the response
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Indicates if the response is successful
        /// </summary>
        public bool IsSuccess { get; set; } = default;

        /// <summary>
        /// The error message if the response is not successful
        /// </summary>
        public string DisplayMessage { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if the response is created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; } = false;

        public DateTime ExpiryDate { get; set; }
    }
}
