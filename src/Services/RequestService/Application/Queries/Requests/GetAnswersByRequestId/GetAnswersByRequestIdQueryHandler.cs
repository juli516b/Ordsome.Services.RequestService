using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Exceptions;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetAnswersByRequestIdQuery, IEnumerable<AnswerDto>>
    {
        private readonly IRequestServiceDbContext _context;

        public GetAnswersByRequestIdQueryHandler(IRequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).FirstOrDefaultAsync(r => r.Id == request.RequestId);

            if (entity == null)
                throw new NotFoundException($"{request.RequestId}", entity);

            List<AnswerDto> listOfAnswers = new List<AnswerDto>();

            foreach (var answer in entity.Answers)
            {
                listOfAnswers.Add(new AnswerDto
                {
                    AnswerId = answer.Id,
                        RequestId = answer.RequestId,
                        TextTranslated = answer.TextTranslated,
                        IsPreferred = answer.IsPreferred,
                });
            }

            return listOfAnswers;
        }
    }
}