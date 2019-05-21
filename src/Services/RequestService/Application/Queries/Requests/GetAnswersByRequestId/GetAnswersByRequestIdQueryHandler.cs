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

namespace Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetAnswersByRequestIdQuery, IEnumerable<AnswerDto>>
    {
        private readonly IRequestServiceDbContext _context;

        public GetAnswersByRequestIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByRequestIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(r => r.Id == request.RequestId,
                    cancellationToken);

            if (entity == null)
                throw new NotFoundException($"{request.RequestId}", request);

            return entity.Answers.Select(RequestMappings.ToAnswerDTO).ToList();
        }
    }
}