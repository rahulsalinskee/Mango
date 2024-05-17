using Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel;

namespace Mango.Services.Coupon.AuthAPI.Repository.Services
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(ApplicationIdentityUserModel applicationUser, IEnumerable<string> roles);
    }
}
