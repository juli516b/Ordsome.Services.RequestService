using FluentValidation;

namespace RequestService.WebApi.Application.Commands.Requests.AnswerCreation
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator()
        {
            RuleFor(x => x.TextTranslated).NotEmpty().NotNull();
        }
    }
}
