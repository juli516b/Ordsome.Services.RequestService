using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Queries.GetAnswersBasedOnUserId
{
    public class GetAnswersBasedOnUserIdQuery : IRequest<ICollection<UserAnswersDto>>
    {
        public Guid UserId { get; set; }
    }
}