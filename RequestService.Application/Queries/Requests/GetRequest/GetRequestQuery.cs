using MediatR;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetRequest
{
    public class GetRequestQuery : IRequest<Request>
    {
        public int Id { get; set; }
    }
}
