using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Queries.GetRequestsBasedOnUserId
{
    public class GetRequestsBasedOnUserIdQuery : IRequest<ICollection<UserRequestsDto>>
    {
        public Guid UserId { get; set; }
    }
}