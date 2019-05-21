using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Commands.Answers.SetAnswerAsCorrectAnswer
{
    public class SetAnswerAsCorrectAnswerCommandHandler : INotificationHandler<SetAnswerAsCorrectAnswerCommand>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        public SetAnswerAsCorrectAnswerCommandHandler(IRequestServiceDbContext context,
            INotificationService notificationService, IMediator mediator)
        {
            _context = context;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task Handle(SetAnswerAsCorrectAnswerCommand notification, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(x => x.Id == notification.RequestId, cancellationToken);

            var answerToEdit = CheckAnswerToEditForNull(notification, entity);

            answerToEdit.IsPreferred = notification.IsPreferred;

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static Answer CheckAnswerToEditForNull(SetAnswerAsCorrectAnswerCommand notification, Request entity)
        {
            if (entity == null) throw new NotFoundException($"{notification.RequestId}", notification);

            var answerToEdit = entity.Answers.FirstOrDefault(a => a.Id == notification.AnswerId);

            if (answerToEdit == null) throw new NotFoundException($"{notification.AnswerId}", notification);

            return answerToEdit;
        }
    }
}