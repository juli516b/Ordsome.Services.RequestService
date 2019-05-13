using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Queries.Requests.GetRequest
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetRequestQuery, RequestPreviewDto>
    {
        private readonly RequestServiceDbContext _context;

        public GetAnswersByRequestIdQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<RequestPreviewDto> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).FirstOrDefaultAsync(r => r.Id == request.RequestId);

            if (entity == null)
            {
                return null;
            }

            return new RequestPreviewDto
            {
                IsClosed = entity.IsClosed,
                    RequestId = entity.Id,
                    LanguageOrigin = entity.LanguageOrigin,
                    LanguageTarget = entity.LanguageTarget,
                    TextToTranslate = entity.TextToTranslate,
                    NoOfAnswers = entity.Answers.Count
            };
        }
    }
}