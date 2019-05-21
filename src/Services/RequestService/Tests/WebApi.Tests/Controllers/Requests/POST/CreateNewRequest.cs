using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Commands.Requests.RequestCreation;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class CreateNewRequest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CreateNewRequest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

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
        public async Task CreateNewRequestWithNoLanguageOrigin_ReturnsSuccessCode()
        {
            var command = new CreateRequestCommand
            {
                LanguageTargetId = 20,
                TextToTranslate = "Bobby har svært ved dansk",
                UserId = Guid.NewGuid()
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests", content);

            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCreateNewRequestCommandsFromDataGenerator), MemberType =
            typeof(TestDataGenerator))]
        public async Task CreateNewRequest_ReturnsBadRequest(CreateRequestCommand a, CreateRequestCommand b,
            CreateRequestCommand c, CreateRequestCommand d, CreateRequestCommand e)
        {
            Assert.True(await IsBadRequest(a));
            Assert.True(await IsBadRequest(b));
            Assert.True(await IsBadRequest(c));
            Assert.True(await IsBadRequest(d));
            Assert.True(await IsBadRequest(e));
        }
        
        private async Task<bool> IsBadRequest(CreateRequestCommand command)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/",
                content);

            return response.StatusCode.ToString() == "BadRequest";
        }
    }
}