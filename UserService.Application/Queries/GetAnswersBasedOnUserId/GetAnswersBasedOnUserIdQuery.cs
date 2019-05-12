using System;
using System.Collections.Generic;
using MediatR;

namespace UserService.Application.Queries.GetAnswersBasedOnUserId
{
    public class GetAnswersBasedOnUserIdQuery : IRequest<ICollection<UserAnswersDto>>
    {
        public Guid UserId { get; set; }
    }
}