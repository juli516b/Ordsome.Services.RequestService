using System;
using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries.Requests.GetRequestsByUserId
{
    public class GetRequestsByUserIdQuery : IRequest<IEnumerable<RequestPreviewDto>>
    {
        public Guid UserId { get; set; }
    }
}