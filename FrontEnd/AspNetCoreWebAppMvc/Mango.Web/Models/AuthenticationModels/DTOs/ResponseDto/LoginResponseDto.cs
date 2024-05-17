namespace Mango.Web.Models.AuthenticationModels.DTOs.ResponseDto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }

        /// <summary>
        /// This Token will be generated with the help of a secrete key (Sitting in appsettings.json)
        /// </summary>
        public string Token { get; set; }
    }
}
