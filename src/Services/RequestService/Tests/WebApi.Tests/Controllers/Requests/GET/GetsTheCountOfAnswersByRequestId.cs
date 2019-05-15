using RequestService.Application.Queries.Requests.GetCountOfAnswersByRequestId;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Tests.Common;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetsTheCountOfAnswersByRequestIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetsTheCountOfAnswersByRequestIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

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
            Random random = new Random();
            int id = random.Next(1000, 2000);
            var response = await _client.GetAsync($"/api/requests/{id}/answers/count");

            var statuscode = response.StatusCode;

            Assert.Equal("NotFound", statuscode.ToString());
        }

    }
}
