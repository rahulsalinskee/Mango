using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.AuthenticationModels.DTOs.RequestDto
{
    public class LoginRequestDto
    {
        /// <summary>
        /// User Name
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
