using TTMS.Models.User.Enums;

namespace TTMS.Models.User.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
