
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DWShop.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> GetToken(IdentityUser user)
        {
            var now = DateTime.UtcNow;
            var key = configuration["identity:key"];

            var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Sub,user.UserName!),
            new(JwtRegisteredClaimNames.Jti,user.Id),
            new(JwtRegisteredClaimNames.Iat,now.ToLocalTime().ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Email,user.Email ?? "NotDefined")

            };

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var encodedJwt = new JwtSecurityTokenHandler();

            var token = encodedJwt.CreateToken(tokenDescriptor);

            return encodedJwt.WriteToken(token);

        }

        public async Task<bool> UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == username);
        }
    }
}
