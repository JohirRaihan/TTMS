using TTMS.Data.Models;
using TTMS.Models.User.Dtos;
namespace TTMS.Domains.Factories
{
    public interface IUserFactory
    {
        UserDto ToUserDto(DimUser user);
        IEnumerable<UserDto> ToUserDtos(IEnumerable<DimUser> users);
    }
}
