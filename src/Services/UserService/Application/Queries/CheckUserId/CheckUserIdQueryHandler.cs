using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UserService.Application.Queries.CheckUserId
{
    public class CheckUserIdQueryHandler : IRequestHandler<CheckUserIdQuery, bool>
    {
        private readonly IUserServiceDbContext _context;
        private readonly IMediator _mediator;

        public CheckUserIdQueryHandler(IUserServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CheckUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}