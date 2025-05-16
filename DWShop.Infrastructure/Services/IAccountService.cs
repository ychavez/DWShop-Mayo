

using Microsoft.AspNetCore.Identity;

namespace DWShop.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<string> GetToken(IdentityUser user);
    }
}
