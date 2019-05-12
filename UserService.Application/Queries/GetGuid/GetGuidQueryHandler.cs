using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserService.Application.Queries.GetGuid
{
    public class GetGuidQueryHandler : IRequestHandler<GetGuidQuery, GetGuidDto>
    {
        private readonly IMediator _mediator;

        public GetGuidQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetGuidDto> Handle(GetGuidQuery request, CancellationToken cancellationToken)
        {
            return new GetGuidDto{
                NewGuid = Guid.NewGuid()
            };
        }
    }
}