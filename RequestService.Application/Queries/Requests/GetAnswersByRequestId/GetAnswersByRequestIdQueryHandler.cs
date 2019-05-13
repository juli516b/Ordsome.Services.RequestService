using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public class GetAnswersByRequestIdQueryHandler : IRequestHandler<GetAnswersByRequestIdQuery, IEnumerable<AnswerDto>>
    {
        private readonly RequestServiceDbContext _context;

        public GetAnswersByRequestIdQueryHandler(RequestServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Requests.Include(a => a.Answers).FirstOrDefaultAsync(r => r.Id == request.RequestId);

            if (entity == null)
                return null;

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

            IEnumerable<AnswerDto> answersToReturn = listOfAnswers;

            return answersToReturn;
        }
    }
}