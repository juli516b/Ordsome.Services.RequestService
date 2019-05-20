using Application.RestClients;
using FluentValidation;
using Ordsome.Services.CrossCuttingConcerns.Languages;

namespace Application.Queries.Requests.GetRequestsByLanguageCode
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
            var listOfLanguages = new ListOfLanguages();

            var language = listOfLanguages.GetLanguageByCode(arg);

            return language == null;
        }
    }
}