using RequestService.Application.Commands.Requests.RequestCreation;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Tests.Common;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class CreateNewRequest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateNewRequest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateNewRequest_ReturnsSuccessStatusCode()
        {
            var command = new CreateRequestCommand
            {
                LanguageOriginId = 40,
                LanguageTargetId = 20,
                TextToTranslate = "Do you want some apples?",
                UserId = Guid.NewGuid()
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateNewRequest_ReturnsBadRequest()
        {
            var command = new CreateRequestCommand
            {
                LanguageOriginId = 40,
                LanguageTargetId = 20,
                TextToTranslate = "",
                UserId = Guid.NewGuid()
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests", content);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
    }
}


