using RequestService.Domain.Requests;

namespace RequestService.Application.Commands.Requests
{
    public class RequestUpdatedEvent
    {
        public Request UpdatedRequest { get; set; }

        public RequestUpdatedEvent (Request updatedRequest)
        {
            UpdatedRequest = updatedRequest;
        }
    }
}