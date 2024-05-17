namespace Mango.Services.Coupon.AuthAPI.Models.DTOs.CommonResponseDtos
{
    public class ResponseDto
    {
        /// <summary>
        /// Get or sets a value indicating whether this <see cref="ResponseDto"/> is success.
        /// </summary>
        public bool IsSuccess { get; set; } = default;

        /// <summary>
        /// Get or sets the result.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Get or sets the display message.
        /// </summary>
        public string DisplayMessage { get; set; } = string.Empty;
    }
}
