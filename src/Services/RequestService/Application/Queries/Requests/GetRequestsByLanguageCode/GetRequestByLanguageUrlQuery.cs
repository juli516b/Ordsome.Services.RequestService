using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Queries.Requests.GetRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Requests.GetAnswersByLanguageCode
{
    public class GetRequestByLanguageUrlQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}
