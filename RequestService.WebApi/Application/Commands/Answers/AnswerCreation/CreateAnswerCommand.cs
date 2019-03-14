using MediatR;
using RequestService.WebApi.Application.Interfaces;
using RequestService.WebApi.Domain.Requests;
using RequestService.WebApi.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Commands.Requests.AnswerCreation
{
    public class CreateAnswerCommand : IRequest
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
    }

    public class Handler : IRequestHandler<CreateAnswerCommand, Unit>
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

        public async Task<Unit> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Answer
            {
                RequestId = request.RequestId,
                TextTranslated = request.TextTranslated
            };

            _context.Answers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await _mediator.Publish(new AnswerCreated { AnswerId = entity.Id }).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
