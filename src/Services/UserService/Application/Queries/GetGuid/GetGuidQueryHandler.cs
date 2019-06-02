using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.GetGuid
{
    public class GetGuidQueryHandler : IRequestHandler<GetGuidQuery, GetGuidDto>
    {
        public async Task<GetGuidDto> Handle(GetGuidQuery request, CancellationToken cancellationToken)
        {
            return new GetGuidDto
            {
                NewGuid = Guid.NewGuid()
            };
        }
    }
}