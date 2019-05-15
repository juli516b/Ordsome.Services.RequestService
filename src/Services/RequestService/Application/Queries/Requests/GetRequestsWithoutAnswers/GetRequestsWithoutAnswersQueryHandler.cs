using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RequestService.Application.Queries.Requests.GetRequestsWithoutAnswers
{
    public class GetRequestsWithoutAnswersQueryHandler : IRequestHandler<GetRequestsWithoutAnswersListQuery, RequestsWithoutAnswersViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IRequestServiceDbContext _context;

        public GetRequestsWithoutAnswersQueryHandler(IRequestServiceDbContext context, IMapper mapper)
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