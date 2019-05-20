using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Queries.Requests.GetRequests;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetRequestByIdTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetRequestByIdTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task ReturnsRequestPreviewDto()
        {
            var response = await _client.GetAsync("/api/requests/1");

            response.EnsureSuccessStatusCode();

            var request = await Utilities.GetResponseContent<RequestPreviewDto>(response);

            Assert.NotNull(request);
        }

        [Fact]
        public async Task ReturnsRightStatusCodeIfRequestIsNotFound()
        {
            var random = new Random();
            var randomInt = random.Next(1000, 2000);
            var response = await _client.GetAsync($"/api/requests/{randomInt}");

            var statusCode = response.StatusCode;

            Assert.Equal("NotFound", statusCode.ToString());
        }
    }
}