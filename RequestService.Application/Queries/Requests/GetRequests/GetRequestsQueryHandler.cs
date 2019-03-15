using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, List<Request>>
    {
        private readonly RequestServiceDbContext _context;

        public GetRequestsQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            //Skal helst bruge automapper
            return await _context.Requests
                .Include(a => a.Answers)
                .ToListAsync().ConfigureAwait(false);
        }
    }
}
