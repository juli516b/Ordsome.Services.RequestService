using MediatR;

namespace Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommand : IRequest
    {
        public int RequestId { get; set; }
        public bool isClosed { get; set; }
    }
}