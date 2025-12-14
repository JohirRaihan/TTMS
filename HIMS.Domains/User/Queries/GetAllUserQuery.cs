using MediatR;
using TTMS.Models.User.Dtos;

namespace TTMS.Domains.User.Queries
{
    public class GetAllUserQuery:IRequest<IEnumerable<UserDto>>
    {
        public GetAllUserQuery()
        {
            
        }
    }
}
