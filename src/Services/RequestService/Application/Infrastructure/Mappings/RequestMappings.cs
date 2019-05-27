using Application.Models;
using Domain.Requests;

namespace Application.Infrastructure.Mappings
{
    public static class RequestMappings
    {
        public static RequestPreviewDto ToRequestPreviewDTO(Request request)
        {
            return new RequestPreviewDto
            {
                RequestId = request.Id,
                LanguageOriginCode = request.LanguageOrigin,
                LanguageTargetCode = request.LanguageTarget,
                NoOfAnswers = request.Answers.Count,
                TextToTranslate = request.TextToTranslate
            };
        }

        public static AnswerDto ToAnswerDTO(Answer answer, string textToTranslate)
        {
            return new AnswerDto
            {
                AnswerId = answer.Id,
                TextTranslated = answer.TextTranslated,
                IsPreferred = answer.IsPreferred,
                RequestId = answer.RequestId,
                TextToTranslate = textToTranslate
            };
        }
    }
}