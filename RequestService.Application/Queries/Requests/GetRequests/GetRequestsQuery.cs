using MediatR;
using RequestService.Domain.Requests;
using System.Collections.Generic;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQuery : IRequest<List<RequestPreviewDto>>
    {
    }
}
