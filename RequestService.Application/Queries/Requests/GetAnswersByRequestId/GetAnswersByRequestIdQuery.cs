using System.Collections.Generic;
using MediatR;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public partial class GetAnswersByRequestIdQuery : IRequest<IEnumerable<AnswerDto>>
    {
        public int Id { get; set; }
    }
}