using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommandValidator : AbstractValidator<AddNewLanguageCommand>
    {
        public AddNewLanguageCommandValidator()
        {
            RuleFor(x => x.LanguageCode).Must(LanguageValidationHelpers.BeALanguageByCode)
                .WithMessage("The language is not a real language. Please try with a real language code");
        }
    }
}