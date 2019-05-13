using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQueryHandler : IRequestHandler<GetCountOfAnswersByRequestIdQuery, CountOfAnswersDto>
    {
        private RequestServiceDbContext _context;

        public GetCountOfAnswersByRequestIdQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<CountOfAnswersDto> Handle(GetCountOfAnswersByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).
            FirstOrDefaultAsync(r => r.Id == request.RequestId);

            CountOfAnswersDto countOfAnswersDto = new CountOfAnswersDto
            {
                noOfAnswers = entity.Answers.Count
            };

            return countOfAnswersDto;
        }
    }
}