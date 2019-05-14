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
    public class GetAllAnswersByRequestIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetAllAnswersByRequestIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllAnswersByRequestId()
        {
            var id = 1;
            var response = await _client.GetAsync($"/api/requests/{id}/answers");

            response.EnsureSuccessStatusCode();

            var answers = await Utilities.GetResponseContent<IEnumerable<AnswerPreviewDto>>(response);

            Assert.NotEmpty(answers);
        }

        [Fact]
        public async Task ReturnsRightStatusCodeIfRequestIdIsNotFound()
        {
            Random random = new Random();
            int randomId = random.Next(2000, 4000);
            var response = await _client.GetAsync($"/api/requests/{randomId}/answers");

            var statuscode = response.StatusCode;

            Assert.Equal("NotFound", statuscode.ToString());
        }
    }
}
