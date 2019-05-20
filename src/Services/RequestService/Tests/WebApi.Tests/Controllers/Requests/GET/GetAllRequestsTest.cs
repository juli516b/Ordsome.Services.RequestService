using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Queries.Requests.GetRequests;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetAllRequestsTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetAllRequestsTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task ReturnsIEnumerableRequestPreviewDto()
        {
            var response = await _client.GetAsync("/api/requests");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }

        [Fact]
        public async Task ReturnsIEnumerableRequestPreviewDto_WithFromAndToLanguage()
        {
            var fromLanguage = "en";
            var toLanguage = "dk";
            var response = await _client.GetAsync($"/api/requests?FromLanguage={fromLanguage}&ToLanguage={toLanguage}");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }
    }
}