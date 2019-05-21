using System;
using System.Threading;
using System.Threading.Tasks;
using Application.RestClients;
using FluentValidation;

namespace Application.Commands.Answers.VoteOnAnswer
{
    public class VoteOnAnswerCommandValidator : AbstractValidator<VoteOnAnswerCommand>
    {
        private readonly IUserServiceClient _client;

        public VoteOnAnswerCommandValidator(IUserServiceClient client)
        {
            _client = client;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("An UserID was not provided")
                .MustAsync(IdExists)
                .WithMessage("ID must be registered");

            RuleFor(x => x.AnswerId).NotNull().NotEmpty().WithMessage("An AnswerID was not provided");
            RuleFor(x => x.RequestId).NotNull().NotEmpty().WithMessage("An RequestId was not provided");
            RuleFor(x => x.Like).NotNull().NotEmpty().WithMessage("A like was not provided");
        }
        //TODO - implement check if the user making the command is already the maker of the answer.
//        private async Task<bool> UserIdIsNotTheSameAsAnswerCreator(VoteOnAnswerCommand arg, CancellationToken cancellationToken)
//        {
//            var request = await _context.Requests.Include(x => x.Answers)
//                .FirstOrDefaultAsync(x => x.Id == arg.RequestId, cancellationToken);
//            var answer = request.Answers.FirstOrDefault(x => x.Id == arg.AnswerId);
//            return answer != null && !answer.UserId.Equals(arg.UserId);
//        }

        private async Task<bool> IdExists(Guid arg1, CancellationToken arg2)
        {
            var exists = await _client.CheckUserId(arg1);
            return exists;
        }
    }
}