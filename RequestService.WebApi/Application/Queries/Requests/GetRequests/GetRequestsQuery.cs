using MediatR;
using RequestService.WebApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQuery : IRequest<List<Request>>
    {
    }
}
