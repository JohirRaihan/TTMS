using HIMS.Models.Department.Dtos;
using MediatR;

namespace TTMS.Domains.Task.Quries
{
    public record GetTasksQuery( TaskStatus? Status, Guid? AssignedToUserId,Guid? TeamId, DateTime? DueDate, string? SortBy = null,
                                bool SortDesc = false, int Page = 1, int PageSize = 10) : IRequest<List<TaskDto>>;

}
