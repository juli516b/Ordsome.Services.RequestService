using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryValidator : AbstractValidator<GetRequestsQuery>
    {
        public GetRequestsQueryValidator()
        {
            RuleFor(x => x.FromLanguage).Must(LanguageValidationHelpers.BeALanguage).Unless(x => x.FromLanguage == null)
                .WithMessage("Choose a correct language");
            RuleFor(x => x.ToLanguage).Must(LanguageValidationHelpers.BeALanguage).Unless(x => x.FromLanguage == null)
                .WithMessage("Choose a correct language");
        }
    }
}