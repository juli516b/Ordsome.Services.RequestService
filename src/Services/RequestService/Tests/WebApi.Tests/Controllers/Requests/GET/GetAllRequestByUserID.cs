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
            var userid = "f5868e20-f943-4027-9cf9-a222df814d1f";
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
