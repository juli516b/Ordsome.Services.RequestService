using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetAnswersByRequestIdQuery, IEnumerable<AnswerDto>>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IRequestMappings _mapper;

        public GetAnswersByRequestIdQueryHandler(IRequestServiceDbContext context, IRequestMappings mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByRequestIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers)
                .FirstOrDefaultAsync(r => r.Id == request.RequestId,
                    cancellationToken);

            if (entity == null)
                throw new NotFoundException($"{request.RequestId}", request);

            List < AnswerDto > answerDtosToReturn = new List<AnswerDto>();
            foreach (var item in entity.Answers)
            {
                answerDtosToReturn.Add(await _mapper.ToAnswerDTOAsync(item));
            }
            return answerDtosToReturn;
        }
    }
}