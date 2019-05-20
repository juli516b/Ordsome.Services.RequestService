using System;
using System.Linq.Expressions;
using Domain.Requests;

namespace Application.Queries.Requests.GetRequests
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