using RequestService.WebApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequests
{
    public class AnswerPreviewDto
    {
        public int AnswerId { get; set; }
        public string TextTranslated { get; set; }

        public static Expression<Func<Answer, AnswerPreviewDto>> Projection
        {
            get
            {
                return a => new AnswerPreviewDto
                {
                    AnswerId = a.Id,
                    TextTranslated = a.TextTranslated
                };
            }
        }
    }
}
