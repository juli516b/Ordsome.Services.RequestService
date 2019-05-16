using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Application.RestClients;
using RequestService.Domain.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Requests.GetAnswersByLanguageCode
{
    public class GetRequestByLanguageUrlQueryHandler : IRequestHandler<GetRequestByLanguageUrlQuery, IEnumerable<RequestPreviewDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;
        private readonly IUserServiceClient _client;

        public GetRequestByLanguageUrlQueryHandler(IRequestServiceDbContext context, IMediator mediator, IUserServiceClient client)
        {
            _context = context;
            _mediator = mediator;
            _client = client;
        }

        public async Task<IEnumerable<RequestPreviewDto>> Handle(GetRequestByLanguageUrlQuery request, CancellationToken cancellationToken)
        {
            var entities = new List<Request>();

            if (LanguageValidationHelpers.BeALanguage(request.FromLanguage) && LanguageValidationHelpers.BeALanguage(request.ToLanguage))
            {
                entities = await _context.Requests.Include(x => x.Answers).Where(x => x.LanguageOrigin == request.FromLanguage && x.LanguageTarget == request.ToLanguage).ToListAsync();
            } else
            {
                entities = await _context.Requests.Include(x => x.Answers).ToListAsync();
            }

            List<RequestPreviewDto> requestPreviewDtos = new List<RequestPreviewDto>();

            foreach (var item in entities)
            {
                requestPreviewDtos.Add(new RequestPreviewDto
                {
                    RequestId = item.Id,
                    LanguageOrigin = item.LanguageOrigin,
                    LanguageTarget = item.LanguageTarget,
                    NoOfAnswers = item.Answers.Count,
                    IsClosed = item.IsClosed
                });
            }
            return requestPreviewDtos;
        }
    }
}
