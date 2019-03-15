using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class RequestPreviewDto
    {
        public RequestPreviewDto()
        {
            Answers = new List<AnswerPreviewDto>();
        }

        public int RequestId { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public List<AnswerPreviewDto> Answers { get; set; }

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
                    Answers = r.Answers.AsQueryable()
                        .Select(AnswerPreviewDto.Projection)
                        .ToList()
                };
            }
        }
    }
}
