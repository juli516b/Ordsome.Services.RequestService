using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.RestClients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Queries.GetRequestsBasedOnUserId
{
    public class
        GetRequestsBasedOnUserIdQueryHandler : IRequestHandler<GetRequestsBasedOnUserIdQuery,
            ICollection<UserRequestsDto>>
    {
        private readonly IRequestServiceClient _client;
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public GetRequestsBasedOnUserIdQueryHandler(IMediator mediator, IUserServiceDbContext context,
            IRequestServiceClient client)
        {
            _context = context;
            _mediator = mediator;
            _client = client;
        }

        public async Task<ICollection<UserRequestsDto>> Handle(GetRequestsBasedOnUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId,
                cancellationToken);

            if (user == null) throw new Exception();
            _client.UserId = request.UserId;

            return JsonConvert.DeserializeObject<List<UserRequestsDto>>(await _client.GetRequests(request.UserId));
        }
    }
}