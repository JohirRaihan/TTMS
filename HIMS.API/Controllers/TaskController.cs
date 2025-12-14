using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TTMS.Domains.Task.Commands;
using TTMS.Domains.Task.Quries;
using TTMS.Models.Task.Dtos;
using TTMS.Models.User.Enums;

namespace TTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);
            var id = await _mediator.Send(new CreateTaskCommand(dto, userId));
            return Ok(id);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UpdateTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);
            var role = Enum.Parse<UserRole>(User.FindFirst(ClaimTypes.Role)!.Value);
            var success = await _mediator.Send(new UpdateTaskCommand(id, dto, userId, role));
            if (!success) return Forbid();
            return Ok();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetTasksQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
