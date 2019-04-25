using MediatR;
using RequestService.Application.Interfaces;
using RequestService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using RequestService.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace RequestService.Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommandHandler
    {
        public class Handler : IRequestHandler<CloseRequestCommand, Unit>
        {
            private readonly RequestServiceDbContext _context;
            private readonly INotificationService _notificationService;
            private readonly IMediator _mediator;

            public Handler(RequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
            {
                _context = context;
                _notificationService = notificationService;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CloseRequestCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.RequestId);

                entity.IsClosed = true;

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
