using System.Collections.Generic;
using Application.Queries.Requests.GetRequests;
using MediatR;

namespace Application.Queries.Requests.GetRequestsByLanguageCode
{
    public class GetRequestByLanguageUrlQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}