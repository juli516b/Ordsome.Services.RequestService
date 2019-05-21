using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries.Requests.GetRequests
{
    public class GetRequestsQuery : IRequest<List<RequestPreviewDto>>
    {
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}