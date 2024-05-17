namespace Mango.Services.Coupon.AuthAPI.Models.JwtModel
{
    /// <summary>
    /// JWT Options class values are set in Program.cs from appsettings.json
    /// </summary>
    public class JWTOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigninKey { get; set; } = string.Empty;
    }
}
