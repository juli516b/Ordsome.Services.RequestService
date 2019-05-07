using FluentValidation;

namespace RequestService.Application.Commands.Answers.AnswerCreation
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator ()
        {
            RuleFor (x => x.TextTranslated).NotEmpty ().NotNull ();
        }
    }
}