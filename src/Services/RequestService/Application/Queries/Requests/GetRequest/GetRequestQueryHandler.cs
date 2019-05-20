using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries.Requests.GetRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetRequest
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetRequestQuery, RequestPreviewDto>
    {
        private readonly IRequestServiceDbContext _context;

        public GetAnswersByRequestIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<RequestPreviewDto> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(r => r.Id == request.RequestId, cancellationToken);

            if (entity == null) throw new NotFoundException($"{request.RequestId}", entity);

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