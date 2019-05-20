using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Queries.Requests.GetRequests;
using Microsoft.AspNetCore.Mvc.Testing;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetRequestsBasedOnLanguageId : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetRequestsBasedOnLanguageId(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task ReturnsAllRequestsBasedOnLanguageCode_ReturnsSuccessCode()
        {
            var languageCode = "en";
            var response = await _client.GetAsync($"/api/requests?languageCode={languageCode}");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }
    }
}