using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.LanguageTargetId).Must(LanguageValidationHelpers.BeALanguageById).WithMessage("Specified language must be a language");
            RuleFor(x => x.TextToTranslate).NotEmpty().WithMessage("Provide a text to translate");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserID is not valid");
        }

    }
}