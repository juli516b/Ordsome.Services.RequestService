using System;
using System.Linq;
using MediatR;

namespace UserService.Application.Queries.CheckUserId
{
    public class CheckUserIdQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}