using MediatR;
using RequestService.Domain.Requests;
using System.Collections.Generic;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public partial class GetAnswersByRequestIdQuery : IRequest<IEnumerable<AnswerDto>>
    {
        public int Id { get; set; }
    }
}

