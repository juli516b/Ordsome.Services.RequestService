using Application.Queries.Requests.GetRequests;
using MediatR;

namespace Application.Queries.Requests.GetRequest
{
    public class GetRequestQuery : IRequest<RequestPreviewDto>
    {
        public int RequestId { get; set; }
    }
}