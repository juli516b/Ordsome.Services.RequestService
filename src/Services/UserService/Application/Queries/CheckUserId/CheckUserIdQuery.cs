using System;
using MediatR;

namespace Application.Queries.CheckUserId
{
    public class CheckUserIdQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}