using TTMS.Data.Models;
using TTMS.Models.User.Dtos;

namespace TTMS.Domains.Factories
{
    public class UserFactory : IUserFactory
    {
        public IEnumerable<UserDto> ToUserDtos(IEnumerable<DimUser> users)
        {
            if (users == null)
            {
                return [];
            }

            var employeeDtos = users.Select(ToUserDto).ToList();
            return employeeDtos;
        }

        public UserDto ToUserDto(DimUser user)
        {
            if (user == null)
            {
                return null;
            }

            var employeeDto = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                UserId = user.Id
            };

            return employeeDto;
        }
    }
}
