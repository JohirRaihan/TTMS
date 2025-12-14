using TTMS.Models.User.Enums;

namespace TTMS.Data.Models
{
    public class DimUser
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }

        public ICollection<FactTask> AssignedTasks { get; set; } = [];
        public ICollection<FactTask> CreatedTasks { get; set; } = [];
    }
}
