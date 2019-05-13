using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserService.Application.RestClients;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Queries.GetRequestsBasedOnUserId
{
    public class GetRequestsBasedOnUserIdQueryHandler : IRequestHandler<GetRequestsBasedOnUserIdQuery, ICollection<UserRequestsDto>>
    {
        private readonly UserServiceDbContext _context;
        private readonly IMediator _mediator;
        private readonly IRequestServiceClient _client;

        public GetRequestsBasedOnUserIdQueryHandler(IMediator mediator, UserServiceDbContext context, IRequestServiceClient client)
        {
            _context = context;
            _mediator = mediator;
            _client = client;
        }

        public async Task<ICollection<UserRequestsDto>> Handle(GetRequestsBasedOnUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                throw new Exception();
            }
            _client.UserId = request.UserId;

            return JsonConvert.DeserializeObject<List<UserRequestsDto>>(await _client.GetRequests(request.UserId));
        }
    }
}