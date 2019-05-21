using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Models;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetsTheCountOfAnswersByRequestIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetsTheCountOfAnswersByRequestIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task GetsTheCountOfAnswersByRequestId()
        {
            var id = 1;

            var response = await _client.GetAsync($"/api/requests/{id}/answers/count");

            response.EnsureSuccessStatusCode();

            var request = await Utilities.GetResponseContent<CountOfAnswersDto>(response);
        }

        [Fact]
        public async Task ReturnsRightStatusCodeIfRequestIdIsNotFound()
        {
            var random = new Random();
            var id = random.Next(1000, 2000);
            var response = await _client.GetAsync($"/api/requests/{id}/answers/count");

            var statusCode = response.StatusCode;

            Assert.Equal("NotFound", statusCode.ToString());
        }
    }
}