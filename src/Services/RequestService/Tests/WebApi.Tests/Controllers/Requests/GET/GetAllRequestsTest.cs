using RequestService.Application.Queries.Requests.GetRequests;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Tests.Common;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.GET
{
    public class GetAllRequestsTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetAllRequestsTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

         [Fact]
         public async Task ReturnsIEnumerableRequestPreviewDto()
        {
            var response = await _client.GetAsync("/api/requests");

            response.EnsureSuccessStatusCode();

            var requests = await Utilities.GetResponseContent<IEnumerable<RequestPreviewDto>>(response);

            Assert.NotEmpty(requests);
        }
    }
}
