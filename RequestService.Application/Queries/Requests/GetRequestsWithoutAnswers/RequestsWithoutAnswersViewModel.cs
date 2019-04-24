using System.Collections.Generic;

namespace RequestService.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class RequestsWithoutAnswersViewModel
    {
        public IList<RequestWithoutAnswersLookupModel> Requests { get; set; }
    }
}
