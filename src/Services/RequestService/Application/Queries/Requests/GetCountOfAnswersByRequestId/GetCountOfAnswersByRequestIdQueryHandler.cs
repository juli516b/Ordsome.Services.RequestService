using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class
        GetCountOfAnswersByRequestIdQueryHandler : IRequestHandler<GetCountOfAnswersByRequestIdQuery, CountOfAnswersDto>
    {
        private readonly IRequestServiceDbContext _context;

        public GetCountOfAnswersByRequestIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<CountOfAnswersDto> Handle(GetCountOfAnswersByRequestIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(r => r.Id == request.RequestId, cancellationToken);

            if (entity == null) throw new NotFoundException($"{request.RequestId}", entity);
            return new CountOfAnswersDto
            {
                noOfAnswers = entity.Answers.Count
            };
        }
    }
}