using Application.Interfaces;
using Application.Models;
using Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Infrastructure.Mappings
{
    public class RequestMappings : IRequestMappings
    {
        private readonly IRequestServiceDbContext _context;

        public RequestMappings(IRequestServiceDbContext context)
        {
            _context = context;
        }
        public RequestPreviewDto ToRequestPreviewDTO(Request request)
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


        public async Task<AnswerDto> ToAnswerDTOAsync(Answer answer)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == answer.RequestId);
            if (request == null)
            {
                throw new NotFoundException(answer.RequestId.ToString(), answer);
            }
            var likes = await _context.AnswerVotes.ToListAsync();
            var amountOfLikes = 0;
            foreach (var like in likes)
            {
                if (like.Like)
                {
                    amountOfLikes++;
                }
            }
            return new AnswerDto
            {
                AnswerId = answer.Id,
                TextTranslated = answer.TextTranslated,
                IsPreferred = answer.IsPreferred,
                RequestId = answer.RequestId,
                TextToTranslate = request.TextToTranslate,
                AmountOfLikes = amountOfLikes
            };
        }
    }
}