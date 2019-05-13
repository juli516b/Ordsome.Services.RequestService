using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;

namespace RequestService.Application.Queries.Answers.GetanswersByUserId
{
    public class GetAnswersByUserIdQuery : IRequest<ICollection<AnswerDto>>
    {
        public Guid UserId { get; set; }
    }
}