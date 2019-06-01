using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Queries.Requests.GetRequest
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetRequestQuery, RequestPreviewDto>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public GetAnswersByRequestIdQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RequestPreviewDto> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(r => r.Id == request.RequestId, cancellationToken);

            if (entity == null) throw new NotFoundException($"{request.RequestId}", request);

            return _mapper.ToRequestPreviewDTO(entity);
        }
    }
}