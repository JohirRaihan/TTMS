using System;
using System.Collections.Generic;
using System.Text;
using TTMS.Models.Team.Dtos;
using TTMS.Models.User.Dtos;

namespace TTMS.Models.Task.Dtos
{
    public class CreateTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }


        public Guid AssignedToUserId { get; set; }
        public UserDto AssignedToUser { get; set; } = null!;


        public Guid CreatedByUserId { get; set; }
        public UserDto CreatedByUser { get; set; } = null!;


        public Guid TeamId { get; set; }
        public TeamDto Team { get; set; } = null!;
    }
}
