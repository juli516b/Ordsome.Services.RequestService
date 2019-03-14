using FluentValidation;

namespace RequestService.WebApi.Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.LagnuageTarget).NotEmpty().NotNull();
            RuleFor(x => x.TextToTranslate).NotEmpty().NotNull();
        }
    }
}
