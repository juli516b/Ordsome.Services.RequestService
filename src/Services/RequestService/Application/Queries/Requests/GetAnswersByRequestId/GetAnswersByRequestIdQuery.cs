using System.Collections.Generic;
using MediatR;

namespace Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQuery : IRequest<IEnumerable<AnswerDto>>
    {
        public int RequestId { get; set; }
    }
}