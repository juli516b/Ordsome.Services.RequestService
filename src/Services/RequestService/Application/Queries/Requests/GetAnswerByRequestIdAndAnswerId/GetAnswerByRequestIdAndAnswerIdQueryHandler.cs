using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Requests.GetAnswerByRequestIdAndAnswerId
{
    public class
        GetAnswerByRequestIdAndAnswerIdQueryHandler : IRequestHandler<GetAnswerByRequestIdAndAnswerIdQuery, AnswerDto>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public GetAnswerByRequestIdAndAnswerIdQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnswerDto> Handle(GetAnswerByRequestIdAndAnswerIdQuery request,
            CancellationToken cancellationToken)
        {
            var requestResult = await _context.Requests.Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            var answer = requestResult.Answers.FirstOrDefault(x => x.Id == request.AnswerId);

            return await _mapper.ToAnswerDTOAsync(answer);
        }
    }
}