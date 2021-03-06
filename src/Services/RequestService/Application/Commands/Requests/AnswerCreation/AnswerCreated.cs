using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.Commands.Requests.AnswerCreation
{
    public class AnswerCreated : INotification
    {
        public int AnswerId { get; set; }

        public class RequestCreatedHandler : INotificationHandler<AnswerCreated>
        {
            //TODO - make a notification to the user who made the request
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