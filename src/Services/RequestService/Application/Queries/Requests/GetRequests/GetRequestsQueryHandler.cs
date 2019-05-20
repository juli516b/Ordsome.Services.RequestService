using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Languages;
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
            var entities = new List<Request>();

            if (string.IsNullOrWhiteSpace(request.FromLanguage) || string.IsNullOrWhiteSpace(request.ToLanguage))
            {
                entities = await _context.Requests.Include(x => x.Answers).ToListAsync();
            }
            else
            {
                entities = await _context.Requests.Include(x => x.Answers).Where(x => x.LanguageOrigin == request.FromLanguage && x.LanguageTarget == request.ToLanguage).ToListAsync();
            }

            List<RequestPreviewDto> requestPreviewDtos = new List<RequestPreviewDto>();

            foreach (var item in entities)
            {
                requestPreviewDtos.Add(new RequestPreviewDto
                {
                    RequestId = item.Id,
                    LanguageOrigin = item.LanguageOrigin,
                    LanguageTarget = item.LanguageTarget,
                    TextToTranslate = item.TextToTranslate,
                    NoOfAnswers = item.Answers.Count,
                    IsClosed = item.IsClosed
                });
            }
            return requestPreviewDtos;
        }
    }
}