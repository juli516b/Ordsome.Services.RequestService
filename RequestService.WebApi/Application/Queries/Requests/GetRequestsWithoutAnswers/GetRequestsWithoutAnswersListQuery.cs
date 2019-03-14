using MediatR;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class GetRequestsWithoutAnswersListQuery : IRequest<RequestsWithoutAnswersViewModel>
    {
    }
}
