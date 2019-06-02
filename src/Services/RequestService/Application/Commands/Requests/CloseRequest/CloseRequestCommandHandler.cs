using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommandHandler
    {
        public class Handler : IRequestHandler<CloseRequestCommand, Unit>
        {
            private readonly IRequestServiceDbContext _context;
            private readonly INotificationService _notificationService;

            public Handler(IRequestServiceDbContext context, INotificationService notificationService)
            {
                _context = context;
                _notificationService = notificationService;
            }

            public async Task<Unit> Handle(CloseRequestCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.RequestId,
                    cancellationToken);

                entity.IsClosed = request.isClosed;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}