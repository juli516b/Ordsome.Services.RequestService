using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.Application.Queries.Requests.GetRequest;
using RequestService.Application.Queries.Requests.GetRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Requests.GetRequests
{
    public class GetRequestsQueryValidator : AbstractValidator<GetRequestsQuery>
    {
        public GetRequestsQueryValidator()
        {
            RuleFor(x => x.FromLanguage).Must(LanguageValidationHelpers.BeALanguage).Unless(x => x.FromLanguage == null).WithMessage("Choose a correct language");
            RuleFor(x => x.ToLanguage).Must(LanguageValidationHelpers.BeALanguage).Unless(x => x.FromLanguage == null).WithMessage("Choose a correct language");
        }
    }
}
