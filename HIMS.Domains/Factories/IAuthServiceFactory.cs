using TTMS.Data.Models;
using TTMS.Models.Auth.Dtos;
using TTMS.Models.User.Dtos;

namespace TTMS.Domains.Factories
{
    public interface IAuthServiceFactory
    {
        Task<DimUser> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
    }
}
