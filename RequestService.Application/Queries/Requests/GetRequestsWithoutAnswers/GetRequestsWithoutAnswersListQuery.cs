using MediatR;

namespace RequestService.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class GetRequestsWithoutAnswersListQuery : IRequest<RequestsWithoutAnswersViewModel>
    {
    }
}
