using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTMS.Domains.Factories;
using TTMS.Domains.User.Queries;
using TTMS.Models.User.Dtos;

namespace TTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User(IMediator mediator, IUserFactory employeeFactory) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IUserFactory _employeeFactory = employeeFactory;

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserDto>>>GetAllUser() { 
            var getUserQuery = new GetAllUserQuery();
            var userDtos = await _mediator.Send(getUserQuery);

            if (userDtos == null) { 
                return NotFound();
            }

            return Ok(userDtos);
        }

    }
}
