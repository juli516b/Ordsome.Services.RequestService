using MediatR;
using RequestService.WebApi.Application.Interfaces;
using RequestService.WebApi.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Commands.Requests.RequestCreation
{
    public class RequestCreated : INotification
    {
        public int RequestId { get; set; }

        public class RequestCreatedHandler : INotificationHandler<RequestCreated>
        {
            private readonly INotificationService _notification;

            public RequestCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(RequestCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message()).ConfigureAwait(false);
            }
        }
    }
}
