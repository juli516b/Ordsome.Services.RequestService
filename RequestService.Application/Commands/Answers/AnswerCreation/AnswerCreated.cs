using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestService.Application.Interfaces;
using RequestService.Application.Models;

namespace RequestService.Application.Commands.Answers.AnswerCreation
{
    public class AnswerCreated : INotification
    {
        public int AnswerId { get; set; }

        public class RequestCreatedHandler : INotificationHandler<AnswerCreated>
        {
            private readonly INotificationService _notification;

            public RequestCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(AnswerCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message()).ConfigureAwait(false);
            }
        }
    }
}