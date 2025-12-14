using MediatR;
using Microsoft.EntityFrameworkCore;
using TTMS.Data.Context;
using TTMS.Data.Models;
using TTMS.Domains.Task.Commands;

namespace TTMS.Domains.Task.Handlers
{
    public class CreateTaskCommandHandler(TTMSContext tTMSContext) : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly TTMSContext _context = tTMSContext;

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new FactTask
            {
                Id = Guid.NewGuid(),
                Title = request.Dto.Title,
                Description = request.Dto.Description,
                AssignedToUserId = request.Dto.AssignedToUserId,
                CreatedByUserId = request.CreatedByUserId,
                TeamId = request.Dto.TeamId,
                DueDate = request.Dto.DueDate,
                Status = request.Dto.Status
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
