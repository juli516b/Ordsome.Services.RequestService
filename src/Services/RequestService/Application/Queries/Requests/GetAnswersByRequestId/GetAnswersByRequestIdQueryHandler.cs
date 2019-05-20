using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
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
                throw new NotFoundException($"{request.RequestId}", entity);

            return entity.Answers.Select(answer => new AnswerDto
                {
                    AnswerId = answer.Id,
                    RequestId = answer.RequestId,
                    TextTranslated = answer.TextTranslated,
                    IsPreferred = answer.IsPreferred
                })
                .ToList();
        }
    }
}