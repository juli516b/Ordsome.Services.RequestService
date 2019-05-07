using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestService.Application.Interfaces;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Commands.Answers.AnswerCreation
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

        public Handler (RequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
        {
            _context = context;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle (CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Answer
            {
                RequestId = request.RequestId,
                TextTranslated = request.TextTranslated
            };

            _context.Answers.Add (entity);

            await _context.SaveChangesAsync (cancellationToken);

            await _mediator.Publish (new AnswerCreated { AnswerId = entity.Id });

            return Unit.Value;
        }
    }
}