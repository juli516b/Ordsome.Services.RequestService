using MediatR;
using RequestService.Domain.Requests;
using System.Collections.Generic;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public partial class GetAnswersByRequestIdQuery : IRequest<ICollection<Answer>>
    {
        public int Id { get; set; }
    }
}

