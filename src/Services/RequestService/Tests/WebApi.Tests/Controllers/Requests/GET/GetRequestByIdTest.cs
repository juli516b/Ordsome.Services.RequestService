using RequestService.Application.Queries.Requests.GetRequests;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Tests.Common;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetRequestByIdTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetRequestByIdTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

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
            Random random = new Random();
            int randomInt = random.Next(1000, 2000);
            var response = await _client.GetAsync($"/api/requests/{randomInt}");

            var statuscode = response.StatusCode;

            Assert.Equal("NotFound", statuscode.ToString());
        }
    }
}
