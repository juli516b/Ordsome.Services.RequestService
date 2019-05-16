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
    public class GetAllRequestByUserIDTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetAllRequestByUserIDTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsIEnumerableRequestPreviewDtoByUserId()
        {
            var userid = "8e3f52d0-ee7e-4353-8941-ab1b75bbdf76";
            var response = await _client.GetAsync($"/api/requests/u/{userid}");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }
        [Fact]
        public async Task ReturnsRightStatusCodeIfUserIdIsNotFound()
        {
            var userid = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/requests/u/{userid}");

            var statuscode = response.StatusCode;

            Assert.Equal("NotFound", statuscode.ToString());
        }

    }
}
