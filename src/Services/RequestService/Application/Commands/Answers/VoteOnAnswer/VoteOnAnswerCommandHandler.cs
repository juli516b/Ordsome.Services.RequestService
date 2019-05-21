using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Application.Commands.Answers.VoteOnAnswer
{
    public class VoteOnAnswerCommandHandler : IRequestHandler<VoteOnAnswerCommand, Unit>
    {
        private readonly IRequestServiceDbContext _context;
        private readonly IMediator _mediator;

        public VoteOnAnswerCommandHandler(IRequestServiceDbContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Unit> Handle(VoteOnAnswerCommand command, CancellationToken cancellationToken)
        {
            var request = await _context.Requests.Include(r => r.Answers)
                .FirstOrDefaultAsync(s => s.Id == command.RequestId, cancellationToken);

            CheckVoteOnAnswerCommandForNull(command, request);

            var answerToSave = new AnswerVote
            {
                AnswerId = command.RequestId,
                UserId = command.UserId,
                Like = command.Like
            };

            await _context.AnswerVotes.AddAsync(answerToSave, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private static void CheckVoteOnAnswerCommandForNull(VoteOnAnswerCommand command, Request request)
        {
            if (request == null) throw new NotFoundException($"{command.RequestId}", command);

            var answer = request.Answers.FirstOrDefault(x => x.Id == command.AnswerId);

            if (answer == null) throw new NotFoundException($"{command.AnswerId}", command);

            if (answer.UserId.Equals(command.UserId)) throw new ForbiddenException($"{command.UserId}", answer);
        }
    }
}