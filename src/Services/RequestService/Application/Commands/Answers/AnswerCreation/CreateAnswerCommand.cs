using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Answers.AnswerCreation
{
    public class CreateAnswerCommand : IRequest<AnswerIdDto>
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<CreateAnswerCommand, AnswerIdDto>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        public Handler(IRequestServiceDbContext context, INotificationService notificationService, IMediator mediator)
        {
            _context = context;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<AnswerIdDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var requestToCheck =
                await _context.Requests.FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);
            if (requestToCheck == null) throw new NotFoundException($"{request.RequestId}", request);

            var entity = new Answer
            {
                RequestId = request.RequestId,
                TextTranslated = request.TextTranslated
            };

            _context.Answers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AnswerCreated {AnswerId = entity.Id});

            return new AnswerIdDto
            {
                Id = request.RequestId
            };
        }
    }
}