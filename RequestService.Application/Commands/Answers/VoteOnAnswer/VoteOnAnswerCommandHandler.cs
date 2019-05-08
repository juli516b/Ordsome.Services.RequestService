using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RequestService.Application.Commands.Answers.VoteOnAnswer
{
    public class VoteOnAnswerCommandHandler : IRequestHandler<VoteOnAnswerCommand, Unit>
    {
        private readonly Infrastructure.Persistence.RequestServiceDbContext _context;
        private readonly IMediator _mediator;
        public VoteOnAnswerCommandHandler(Infrastructure.Persistence.RequestServiceDbContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Unit> Handle(VoteOnAnswerCommand command, CancellationToken cancellationToken)
        {

            var request = await _context.Requests.Include(r => r.Answers).FirstOrDefaultAsync(s => s.Id == command.RequestId);

            if (request == null)
            {
                throw new Exception();
            }

            var answer = request.Answers.FirstOrDefault(x => x.Id == command.AnswerId);

            if (answer == null)
            {
                throw new Exception();
            }

            bool answerContainsRightUserId = answer.UserId.Equals(command.UserId);

            if (answerContainsRightUserId == true)
            {
                throw new Exception();
            }

            var answerToSave = new Domain.Requests.AnswerVote
            {
                AnswerId = command.RequestId,
                UserId = command.UserId,
                Like = command.Like,
            };

            await _context.AnswerVotes.AddAsync(answerToSave);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}