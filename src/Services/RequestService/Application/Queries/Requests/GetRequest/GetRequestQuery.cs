using Application.Models;
using MediatR;

namespace Application.Queries.Requests.GetRequest
{
    public class GetRequestQuery : IRequest<RequestPreviewDto>
    {
        public int RequestId { get; set; }
    }
}