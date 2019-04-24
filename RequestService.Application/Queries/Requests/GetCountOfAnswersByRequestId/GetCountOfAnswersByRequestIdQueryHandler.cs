using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId
{
    public class GetCountOfAnswersByRequestIdQueryHandler : IRequestHandler<GetCountOfAnswersByRequestIdQuery, int>
    {
        private RequestServiceDbContext _context;

        public GetCountOfAnswersByRequestIdQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetCountOfAnswersByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).
                FirstOrDefaultAsync(r => r.Id == request.Id);

            return entity.Answers.Count;
        }
    }
}
