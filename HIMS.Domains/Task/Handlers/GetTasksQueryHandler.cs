using HIMS.Models.Department.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTMS.Data.Context;
using TTMS.Domains.Task.Quries;

namespace TTMS.Domains.Task.Handlers
{
    public class GetTasksQueryHandler(TTMSContext context) : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly TTMSContext _context = context;

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tasks.AsQueryable();

            if (request.Status.HasValue) query = query.Where(x => x.Status == request.Status);
            if (request.AssignedToUserId.HasValue) query = query.Where(x => x.AssignedToUserId == request.AssignedToUserId);
            if (request.TeamId.HasValue) query = query.Where(x => x.TeamId == request.TeamId);
            if (request.DueDate.HasValue) query = query.Where(x => x.DueDate.Date == request.DueDate.Value.Date);

            // Sorting
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "duedate" => request.SortDesc ? query.OrderByDescending(x => x.DueDate) : query.OrderBy(x => x.DueDate),
                    "title" => request.SortDesc ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                    _ => query
                };
            }

            // Pagination
            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            // Project FactTask to TaskDto
            return await query
                .Select(x => new TaskDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    DueDate = x.DueDate,
                    AssignedToUserId = x.AssignedToUserId,
                    CreatedByUserId = x.CreatedByUserId,
                    TeamId = x.TeamId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
