namespace Mango.Services.Coupon.AuthAPI.Models.DTOs.RequestDtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
