using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, List<RequestPreviewDto>>
    {
        private readonly IRequestServiceDbContext _context;

        public GetRequestsQueryHandler(IRequestServiceDbContext context)
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
                            NoOfAnswers = entity.Answers.Count,
                            LanguageOrigin = entity.LanguageOrigin,
                            LanguageTarget = entity.LanguageTarget,
                            TextToTranslate = entity.TextToTranslate,
                            IsClosed = entity.IsClosed,
                            UserId = entity.UserId
                    });
            }
            return entitiesToReturn;
        }
    }
}