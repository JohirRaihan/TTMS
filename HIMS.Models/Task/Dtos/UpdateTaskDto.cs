using TTMS.Models.Team.Dtos;

namespace TTMS.Models.Task.Dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }

        public Guid AssignedToUserId { get; set; }

        public Guid TeamId { get; set; }
        public TeamDto Team { get; set; } = null!;
    }
}
