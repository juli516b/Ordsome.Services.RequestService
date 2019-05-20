using System;
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
    public class GetAllAnswersByRequestIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public GetAllAnswersByRequestIdTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

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
            var random = new Random();
            var randomId = random.Next(2000, 4000);
            var response = await _client.GetAsync($"/api/requests/{randomId}/answers");

            var statusCode = response.StatusCode;

            Assert.Equal("NotFound", statusCode.ToString());
        }
    }
}