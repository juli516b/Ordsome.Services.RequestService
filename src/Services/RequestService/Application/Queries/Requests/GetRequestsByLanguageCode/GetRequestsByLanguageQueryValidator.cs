using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.Application.RestClients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Requests.GetAnswersByLanguageCode
{
    public class GetRequestsByLanguageQueryValidator : AbstractValidator<GetRequestByLanguageUrlQuery>
    {
        private readonly IUserServiceClient _client;

        public GetRequestsByLanguageQueryValidator(IUserServiceClient client)
        {
            _client = client;
            RuleFor(x => x.FromLanguage).Must(BeALanguage);
            RuleFor(x => x.ToLanguage).Must(BeALanguage);

        }

        private bool BeALanguage(string arg)
        {
            ListOfLanguages listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguageByCode(arg);

            return language == null;
        }
    }
}
