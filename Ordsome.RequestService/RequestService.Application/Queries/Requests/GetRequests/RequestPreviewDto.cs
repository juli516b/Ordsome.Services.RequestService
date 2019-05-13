using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class RequestPreviewDto
    {
        public RequestPreviewDto() { }

        public int RequestId { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public int NoOfAnswers { get; set; }
        public bool IsClosed { get; set; }
        public static Expression<Func<Request, RequestPreviewDto>> Projection
        {
            get
            {
                return r => new RequestPreviewDto
                {
                    RequestId = r.Id,
                    LanguageOrigin = r.LanguageOrigin,
                    LanguageTarget = r.LanguageTarget,
                    TextToTranslate = r.TextToTranslate,
                    NoOfAnswers = r.Answers.Count
                };
            }
        }
    }
}