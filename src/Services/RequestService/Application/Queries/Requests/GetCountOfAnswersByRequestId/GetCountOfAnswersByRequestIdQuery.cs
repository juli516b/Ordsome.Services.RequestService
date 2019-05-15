using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQuery : IRequest<CountOfAnswersDto>
    {
        public int RequestId { get; set; }

    }
}