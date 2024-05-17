using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel
{
    public class ApplicationIdentityUserModel : IdentityUser
    {
        /// <summary>
        /// This Name column will be added to the table with existing columns of
        /// AspNetUsers table provided by IdentityUser table from Identity Framework Core
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
