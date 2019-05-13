using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Exceptions;
using RequestService.Application.Interfaces;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Commands.Answers.AnswerCreation
{
    public class CreateAnswerCommand : IRequest<AnswerIdDto>
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
    }

    public class Handler : IRequestHandler<CreateAnswerCommand, AnswerIdDto>
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

        public async Task<AnswerIdDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var requestToCheck = await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.RequestId);
            if (requestToCheck == null)
            {
                throw new NotFoundException($"{request.RequestId}", request);
            }

            var entity = new Answer
            {
                RequestId = request.RequestId,
                TextTranslated = request.TextTranslated
            };

            _context.Answers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AnswerCreated { AnswerId = entity.Id });

            return new AnswerIdDto
            {
                Id = request.RequestId
            };
        }
    }
}