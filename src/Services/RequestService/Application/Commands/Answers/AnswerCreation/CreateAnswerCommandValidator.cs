using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RequestService.Application.RestClients;

namespace RequestService.Application.Commands.Answers.AnswerCreation
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        private readonly IUserServiceClient _client;

        public CreateAnswerCommandValidator(IUserServiceClient client)
        {
            _client = client;

            RuleFor(x => x.TextTranslated).NotEmpty().NotNull();
            RuleFor(x => x.RequestId).NotEmpty().NotNull();
            RuleFor(x => x.UserId).MustAsync(IdExists).WithMessage("ID must be registered");
        }

        private async Task<bool> IdExists(Guid arg1, CancellationToken arg2)
        {
            bool exists = await _client.CheckUserId(arg1);
            return exists;
        }
    }
}