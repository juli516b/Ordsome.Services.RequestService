using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, List<RequestPreviewDto>>
    {
        private readonly RequestServiceDbContext _context;

        public GetRequestsQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestPreviewDto>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Requests.Include(a => a.Answers).ToListAsync();

            List<RequestPreviewDto> entitiesToReturn = new List<RequestPreviewDto>();

            foreach (var entity in entities)
            {
                entitiesToReturn.Add(
                    new RequestPreviewDto
                    {
                        RequestId = entity.Id,
                        noOfAnswers = entity.Answers.Count,
                        LanguageOrigin = entity.LanguageOrigin,
                        LanguageTarget = entity.LanguageTarget,
                        TextToTranslate = entity.TextToTranslate
                    });
            }
            return entitiesToReturn;
        }
    }
}
