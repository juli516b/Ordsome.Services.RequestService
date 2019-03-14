using RequestService.WebApi.Domain.Requests;

namespace RequestService.WebApi.Domain.Events.Requests
{
    public class RequestUpdatedEvent
    {
        public Request UpdatedRequest { get; set; }

        public RequestUpdatedEvent(Request updatedRequest)
        {
            UpdatedRequest = updatedRequest;
        }
    }
}
