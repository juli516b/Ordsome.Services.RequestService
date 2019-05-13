using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RequestService.Application.Exceptions;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;
using RequestService.Infrastructure.Persistence;

namespace RequestService.Application.Queries.Answers.GetanswersByUserId
{
    public class GetAnswersByUserIdQueryHandler : IRequestHandler<GetAnswersByUserIdQuery, ICollection<AnswerDto>>
    {
        private readonly RequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public GetAnswersByUserIdQueryHandler(RequestServiceDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ICollection<AnswerDto>> Handle(GetAnswersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.Answers.Where(x => x.UserId == request.UserId).ToListAsync();

            if (answers == null)
            {
                throw new NotFoundException($"{request.UserId}", answers);
            }

            ICollection<AnswerDto> answersToReturn = new List<AnswerDto>();
            foreach (var item in answers)
            {
                {
                    answersToReturn.Add(new AnswerDto
                    {
                        AnswerId = item.Id,
                            IsPreferred = item.IsPreferred,
                            RequestId = item.RequestId,
                            TextTranslated = item.TextTranslated
                    });
                }
            }
            return answersToReturn;
        }
    }
}