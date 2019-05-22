using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries.Requests.CheckRequest
{
    public class CheckRequestQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public string TextToTranslate { get; set; }
    }
}