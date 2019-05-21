using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetLanguages : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetLanguages(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task GetLanguages_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/requests/languages/");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<LanguageDto>>(response);

            Assert.NotEmpty(requests);
        }
    }
}