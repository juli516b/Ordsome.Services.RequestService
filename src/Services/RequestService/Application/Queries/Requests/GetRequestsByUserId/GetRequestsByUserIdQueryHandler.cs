using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetRequestsByUserId
{
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
            return requests.Count == 0
                ? throw new NotFoundException($"{request.UserId}", request)
                : requests.Select(RequestMappings.ToRequestPreviewDTO).ToList();
        }
    }
}