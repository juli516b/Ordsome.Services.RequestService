using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries.Requests.GetRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetRequestsByUserId
{
    public class GetRequestsByUserIdQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public Guid UserId { get; set; }
    }

    public class
        GetRequestsByUserIdQueryHandler : IRequestHandler<GetRequestsByUserIdQuery, IEnumerable<RequestPreviewDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private IMediator _mediator;

        public GetRequestsByUserIdQueryHandler(IRequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<RequestPreviewDto>> Handle(GetRequestsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var requests = await _context.Requests.Where(x => x.UserId == request.UserId).Include(x => x.Answers)
                .ToListAsync(cancellationToken);
            if (requests.Count == 0) throw new NotFoundException($"{request.UserId}", request);
            var requestsToReturn = new List<RequestPreviewDto>();
            foreach (var item in requests)
                requestsToReturn.Add(new RequestPreviewDto
                {
                    RequestId = item.Id,
                    LanguageOrigin = item.LanguageOrigin,
                    LanguageTarget = item.LanguageTarget,
                    TextToTranslate = item.TextToTranslate,
                    NoOfAnswers = item.Answers.Count,
                    IsClosed = item.IsClosed
                });

            return requestsToReturn;
        }
    }
}