using MediatR;
using RequestService.WebApi.Application.Interfaces;
using RequestService.WebApi.Domain.Requests;
using RequestService.WebApi.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommand : IRequest
    {
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }

    public class Handler : IRequestHandler<CreateRequestCommand, Unit>
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

        public async Task<Unit> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = new Request
            {
                LanguageTarget = request.LanguageTarget,
                LanguageOrigin = request.LanguageOrigin,
                TextToTranslate = request.TextToTranslate
            };

            _context.Requests.Add(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await _mediator.Publish(new RequestCreated { RequestId = entity.Id }).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
