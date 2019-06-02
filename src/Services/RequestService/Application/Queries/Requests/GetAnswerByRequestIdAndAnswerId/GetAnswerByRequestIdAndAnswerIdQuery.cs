using Application.Models;
using MediatR;

namespace Application.Queries.Requests.GetAnswerByRequestIdAndAnswerId
{
    public class GetAnswerByRequestIdAndAnswerIdQuery : IRequest<AnswerDto>
    {
        public int AnswerId { get; set; }
        public int RequestId { get; set; }
    }
}