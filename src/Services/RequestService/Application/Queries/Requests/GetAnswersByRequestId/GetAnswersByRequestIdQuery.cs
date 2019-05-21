using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQuery : IRequest<IEnumerable<AnswerDto>>
    {
        public int RequestId { get; set; }
    }
}