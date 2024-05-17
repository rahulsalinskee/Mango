namespace Mango.Services.ProductAPIs.DTOs.CommonResponseDtos
{
    public class ResponseDto
    {
        /// <summary>
        /// Result
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; } = default;

        /// <summary>
        /// DisplayMessage
        /// </summary>
        public string DisplayMessage { get; set; } = string.Empty;
    }
}
