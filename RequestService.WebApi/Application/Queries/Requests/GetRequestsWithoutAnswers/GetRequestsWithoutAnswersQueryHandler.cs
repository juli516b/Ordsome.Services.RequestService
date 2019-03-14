using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.WebApi.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class GetRequestsWithoutAnswersQueryHandler : IRequestHandler<GetRequestsWithoutAnswersListQuery, RequestsWithoutAnswersViewModel>
    {
        private readonly IMapper _mapper;
        private readonly RequestServiceDbContext _context;

        public GetRequestsWithoutAnswersQueryHandler(RequestServiceDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RequestsWithoutAnswersViewModel> Handle(GetRequestsWithoutAnswersListQuery request, CancellationToken cancellationToken)
        {
            return new RequestsWithoutAnswersViewModel
            {
                Requests = await _context.Requests.ProjectTo<RequestWithoutAnswersLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken).ConfigureAwait(false)
            };
        }
    }
}
