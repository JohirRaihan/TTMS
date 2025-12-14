using MediatR;
using TTMS.Data.Context;
using TTMS.Domains.Factories;
using TTMS.Domains.User.Queries;
using TTMS.Models.User.Dtos;

namespace TTMS.Domains.User.Handlers
{
    public class GetAllUserQueryHandler(TTMSContext hIMSContext, IUserFactory employeeFactory) : IRequestHandler<GetAllUserQuery, IEnumerable<UserDto>>
    {
        private readonly TTMSContext _context = hIMSContext;
        private readonly IUserFactory _userFactory = employeeFactory;

        public async Task<IEnumerable<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var employees = _context.Users.ToList();

            return _userFactory.ToUserDtos(employees);
        }
    }
}
