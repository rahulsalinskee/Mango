namespace Mango.Services.Coupon.AuthAPI.Models.DTOs.ErrorDtos
{
    public class ErrorDto
    {
        /// <summary>
        /// If Error Message is not empty, that means there is an error in doing some operation
        /// If Error Message is empty, that means operation which is doing is successful
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
