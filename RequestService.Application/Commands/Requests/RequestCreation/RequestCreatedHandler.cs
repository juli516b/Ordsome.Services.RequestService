using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RequestService.Application.Interfaces;
using RequestService.Application.Models;

namespace RequestService.Application.Commands.Requests.RequestCreation
{
    public partial class RequestCreated
    {
        public class RequestCreatedHandler : INotificationHandler<RequestCreated>
        {
            private readonly INotificationService _notification;

            public RequestCreatedHandler (INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle (RequestCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync (new Message ()).ConfigureAwait (false);
            }
        }
    }
}