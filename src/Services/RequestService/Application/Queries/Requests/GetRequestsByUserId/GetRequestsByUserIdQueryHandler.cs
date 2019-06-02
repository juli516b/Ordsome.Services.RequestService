using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Queries.Requests.GetRequestsByUserId
{
    public class
        GetRequestsByUserIdQueryHandler : IRequestHandler<GetRequestsByUserIdQuery, IEnumerable<RequestPreviewDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public GetRequestsByUserIdQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestPreviewDto>> Handle(GetRequestsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var requests = await _context.Requests.Where(x => x.UserId == request.UserId).Include(x => x.Answers)
                .ToListAsync(cancellationToken);
            return requests.Count == 0
                ? throw new NotFoundException($"{request.UserId}", request)
                : requests.Select(_mapper.ToRequestPreviewDTO).ToList();
        }
    }
}