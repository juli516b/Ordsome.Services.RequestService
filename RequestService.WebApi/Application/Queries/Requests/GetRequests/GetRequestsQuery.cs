using MediatR;
using RequestService.WebApi.Domain.Requests;
using System.Collections.Generic;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQuery : IRequest<List<Request>>
    {
    }
}
