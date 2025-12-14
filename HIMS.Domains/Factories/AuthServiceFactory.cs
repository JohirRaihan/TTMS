using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTMS.Data.Context;
using TTMS.Data.Models;
using TTMS.Models.Auth.Dtos;
using TTMS.Models.User.Dtos;

namespace TTMS.Domains.Factories
{
    public class AuthServiceFactory(TTMSContext userDbContext, IConfiguration configuration) : IAuthServiceFactory
    {

        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await userDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user is null)
            {
                return null;
            }

            return await GenerateTokensAsync(user);
        }



        public async Task<DimUser> RegisterAsync(UserDto request)
        {
            if (await userDbContext.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }

            var user = new DimUser();
          

            user.Email = request.Email;
            user.FullName = request.FullName;
            user.Role = request.Role;
            userDbContext.Users.Add(user);
            await userDbContext.SaveChangesAsync();

            return user;
        }

        private async Task<TokenResponseDto> GenerateTokensAsync(DimUser user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateAccessToken(user)
            };
        }

        private string CreateAccessToken(DimUser user)
        {
            var userRoleString = user.Role.ToString();

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            
            new(ClaimTypes.Role, userRoleString)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
