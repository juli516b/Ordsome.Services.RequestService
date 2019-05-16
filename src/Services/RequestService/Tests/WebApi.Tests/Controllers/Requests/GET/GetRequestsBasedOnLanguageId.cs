using RequestService.Application.Queries.Requests.GetRequests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Tests.Common;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetRequestsBasedOnLanguageId : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetRequestsBasedOnLanguageId(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsAllRequestsBasedOnLanguageCode_ReturnsSuccessCode()
        {
            string languageCode = "en";
            var response = await _client.GetAsync($"/api/requests?languageCode={languageCode}");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }
    }
}
