using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Application.Commands.Requests.CloseRequest
{
    public class CloseRequestCommand : IRequest
    {
        public bool IsClosed { get; set; }
    }
}
