using FluentValidation;

namespace RequestService.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator ()
        {
            RuleFor (x => x.LanguageTargetlId).NotEmpty ().NotNull ();
            RuleFor (x => x.TextToTranslate).NotEmpty ().NotNull ();
        }
    }
}