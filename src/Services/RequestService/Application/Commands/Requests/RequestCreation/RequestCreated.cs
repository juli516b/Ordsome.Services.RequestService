using MediatR;

namespace Application.Commands.Requests.RequestCreation
{
    public partial class RequestCreated : INotification
    {
        public int RequestId { get; set; }
    }
}