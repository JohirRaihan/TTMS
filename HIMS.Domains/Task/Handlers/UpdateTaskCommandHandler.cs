using MediatR;
using TTMS.Data.Context;
using TTMS.Domains.Task.Commands;
using TTMS.Models.User.Enums;

namespace TTMS.Domains.Task.Handlers
{
    public class UpdateTaskCommandHandler(TTMSContext context) : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly TTMSContext _context = context;

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync([request.TaskId], cancellationToken: cancellationToken);
            if (task == null) return false;


            // Role-based restrictions
            if (request.CurrentUserRole == UserRole.Employee && task.AssignedToUserId != request.CurrentUserId)
                return false;


            if (request.CurrentUserRole == UserRole.Employee)
                task.Status = request.Dto.Status;
            else // Admin/Manager can update all fields
            {
                task.Title = request.Dto.Title;
                task.Description = request.Dto.Description;
                task.AssignedToUserId = request.Dto.AssignedToUserId;
                task.DueDate = request.Dto.DueDate;
                task.Status = request.Dto.Status;
            }


            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
