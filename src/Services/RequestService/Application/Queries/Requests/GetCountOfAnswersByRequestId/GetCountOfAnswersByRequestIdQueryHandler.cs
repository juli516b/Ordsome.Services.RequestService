using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Exceptions;

namespace RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQueryHandler : IRequestHandler<GetCountOfAnswersByRequestIdQuery, CountOfAnswersDto>
    {
        private IRequestServiceDbContext _context;

        public GetCountOfAnswersByRequestIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<CountOfAnswersDto> Handle(GetCountOfAnswersByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).FirstOrDefaultAsync(r => r.Id == request.RequestId);

            if (entity == null)
            {
                throw new NotFoundException($"{request.RequestId}", entity);
            }
            return new CountOfAnswersDto
            {
                noOfAnswers = entity.Answers.Count
            };
        }
    }
}