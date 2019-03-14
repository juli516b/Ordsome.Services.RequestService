using RequestService.WebApi.Domain.Requests;
using RequestService.WebApi.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
