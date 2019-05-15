using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Interfaces;

namespace RequestService.Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommandHandler
    {
        public class Handler : IRequestHandler<CloseRequestCommand, Unit>
        {
            private readonly IRequestServiceDbContext _context;
            private readonly INotificationService _notificationService;
            private readonly IMediator _mediator;

            public Handler(IRequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
            {
                _context = context;
                _notificationService = notificationService;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CloseRequestCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.RequestId);

                entity.IsClosed = request.isClosed;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}