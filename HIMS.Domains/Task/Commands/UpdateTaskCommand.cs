using MediatR;
using TTMS.Models.Task.Dtos;
using TTMS.Models.User.Enums;

namespace TTMS.Domains.Task.Commands
{
    public class UpdateTaskCommand(Guid TaskId, UpdateTaskDto Dto, Guid CurrentUserId, UserRole CurrentUserRole) : IRequest<bool>
    {
        public Guid TaskId { get; } = TaskId;
        public UpdateTaskDto Dto { get; } = Dto;
        public Guid CurrentUserId { get; } = CurrentUserId;
        public UserRole CurrentUserRole { get; } = CurrentUserRole;
    }
}
