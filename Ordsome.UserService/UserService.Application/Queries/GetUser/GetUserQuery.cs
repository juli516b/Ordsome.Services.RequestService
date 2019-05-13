using System;
using MediatR;

namespace UserService.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
    }
}