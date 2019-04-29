using MediatR;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetRequest
{
    public class GetRequestQuery : IRequest<RequestPreviewDto>
    {
        public int RequestId { get; set; }
    }
}
