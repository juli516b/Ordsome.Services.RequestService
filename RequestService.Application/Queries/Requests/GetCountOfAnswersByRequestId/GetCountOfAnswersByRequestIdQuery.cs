using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQuery : IRequest<int>
    {
        public int Id { get; set; }

    }
}
