using FluentValidation;

namespace RequestService.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.LanguageTarget).NotEmpty().NotNull();
            RuleFor(x => x.TextToTranslate).NotEmpty().NotNull();
        }
    }
}
