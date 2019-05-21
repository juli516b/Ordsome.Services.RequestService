using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using Domain.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetRequests
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
            var entities = await CheckGetRequestsQueryParams(request, cancellationToken);

            var requestPreviewDtos = new List<RequestPreviewDto>();

            foreach (var item in entities)
                requestPreviewDtos.Add(RequestMappings.ToRequestPreviewDTO(item));
            return requestPreviewDtos;
        }

        private async Task<List<Request>> CheckGetRequestsQueryParams(GetRequestsQuery request,
            CancellationToken cancellationToken)
        {
            // TODO - make this a switch statement.
            List<Request> entities = null;

            if (string.IsNullOrWhiteSpace(request.FromLanguage) && string.IsNullOrWhiteSpace(request.ToLanguage))
                entities = await _context.Requests.Include(x => x.Answers)
                    .ToListAsync(cancellationToken);
            else if (!string.IsNullOrWhiteSpace(request.FromLanguage) && string.IsNullOrWhiteSpace(request.ToLanguage))
                entities = await _context.Requests.Include(x => x.Answers)
                    .Where(x => x.LanguageOrigin == request.FromLanguage)
                    .ToListAsync(cancellationToken);
            else if (string.IsNullOrWhiteSpace(request.FromLanguage) && !string.IsNullOrWhiteSpace(request.ToLanguage))
                entities = await _context.Requests.Include(x => x.Answers)
                    .Where(x => x.LanguageTarget == request.ToLanguage)
                    .ToListAsync(cancellationToken);
            else if (!string.IsNullOrWhiteSpace(request.FromLanguage) && !string.IsNullOrWhiteSpace(request.ToLanguage))
                entities = await _context.Requests.Include(x => x.Answers).Where(x =>
                        x.LanguageOrigin == request.FromLanguage && x.LanguageTarget == request.ToLanguage)
                    .ToListAsync(cancellationToken);
            return entities;
        }
    }
}