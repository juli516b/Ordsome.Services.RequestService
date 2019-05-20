using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.RestClients;

namespace UserService.Application.Queries.GetAnswersBasedOnUserId
{
    public class GetAnswersBasedOnUserIdQueryHandler : IRequestHandler<GetAnswersBasedOnUserIdQuery, ICollection<UserAnswersDto>>
    {
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;
        private readonly IRequestServiceClient _client;

        public GetAnswersBasedOnUserIdQueryHandler(IMediator mediator, IUserServiceDbContext context, IRequestServiceClient client)
        {
            _context = context;
            _mediator = mediator;
            _client = client;
        }
        public async Task<ICollection<UserAnswersDto>> Handle(GetAnswersBasedOnUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                throw new NotFoundException(request.UserId.ToString(), user);
            }
            _client.UserId = request.UserId;

            var resultToReturn = JsonConvert.DeserializeObject<List<UserAnswersDto>>(await _client.GetAnswers(request.UserId));

            foreach (var item in resultToReturn)
            {
                if (item.TextTranslated == null)
                {
                    throw new NotFoundException(item.TextTranslated, item);
                }
            }

            return resultToReturn;
        }
    }
}