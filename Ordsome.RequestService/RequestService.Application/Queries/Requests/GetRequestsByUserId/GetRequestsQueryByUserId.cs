using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Queries.Requests.GetRequestsByUserId
{
    public class GetRequestsByUserIdQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public Guid UserId { get; set; }
    }

    public class GetRequestsByUserIdQueryHandler : IRequestHandler<GetRequestsByUserIdQuery, IEnumerable<RequestPreviewDto>>
    {
        private readonly RequestServiceDbContext _context;
        private IMediator _mediator;

        public GetRequestsByUserIdQueryHandler(RequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<RequestPreviewDto>> Handle(GetRequestsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests.Include(x => x.Answers).ToListAsync(cancellationToken);
            List<RequestPreviewDto> requestsToReturn = new List<RequestPreviewDto>();
            foreach (var item in requests)
            {
                if (item.UserId == request.UserId)
                {
                    requestsToReturn.Add(new RequestPreviewDto
                    {
                        RequestId = item.Id,
                            LanguageOrigin = item.LanguageOrigin,
                            LanguageTarget = item.LanguageTarget,
                            TextToTranslate = item.TextToTranslate,
                            NoOfAnswers = item.Answers.Count,
                            IsClosed = item.IsClosed
                    });
                }
            }
            return requestsToReturn;
        }
    }
}