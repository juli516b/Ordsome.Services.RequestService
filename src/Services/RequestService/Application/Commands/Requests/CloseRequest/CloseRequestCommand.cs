using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RequestService.Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommand : IRequest
    {
        public int RequestId { get; set; }
        public bool isClosed { get; set; }
    }
}