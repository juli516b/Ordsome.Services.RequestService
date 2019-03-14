using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class GetRequestsWithoutAnswersListQuery : IRequest<RequestsWithoutAnswersViewModel>
    {
    }
}
