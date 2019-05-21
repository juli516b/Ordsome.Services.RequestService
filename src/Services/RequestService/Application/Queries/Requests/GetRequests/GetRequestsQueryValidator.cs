using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryValidator : AbstractValidator<GetRequestsQuery>
    {
        public GetRequestsQueryValidator()
        {
            RuleFor(x => x.FromLanguage).Must(LanguageValidationHelpers.BeALanguageByCode)
                .When(x => !string.IsNullOrWhiteSpace(x.FromLanguage))
                .WithMessage("Choose a correct language");

            RuleFor(x => x.ToLanguage).Must(LanguageValidationHelpers.BeALanguageByCode)
                .When(x => !string.IsNullOrWhiteSpace(x.ToLanguage))
                .WithMessage("Choose a correct language");
        }
    }
}