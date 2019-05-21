using Domain.Requests;

namespace Application.Commands.Requests
{
    public class RequestUpdatedEvent
    {
        public RequestUpdatedEvent(Request updatedRequest)
        {
            UpdatedRequest = updatedRequest;
        }

        public Request UpdatedRequest { get; set; }
    }
}