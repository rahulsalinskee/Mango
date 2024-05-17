using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.AuthenticationModels.DTOs.RequestDto
{
    public class RegistrationRequestDto
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

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Role
        /// </summary>
        public string? Role { get; set; }
    }
}
