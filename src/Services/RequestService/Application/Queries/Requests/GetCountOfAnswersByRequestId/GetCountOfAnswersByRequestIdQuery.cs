using MediatR;

namespace Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQuery : IRequest<CountOfAnswersDto>
    {
        public int RequestId { get; set; }
    }
}