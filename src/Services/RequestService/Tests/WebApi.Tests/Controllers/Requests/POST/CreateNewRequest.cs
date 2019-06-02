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

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCreateNewRequestCommandsFromDataGenerator), MemberType =
            typeof(TestDataGenerator))]
        public async Task CreateNewRequest_ReturnsBadRequest(
            CreateRequestCommand a, CreateRequestCommand b, CreateRequestCommand c)
        {
            Assert.True(await CheckStatusCode(a, "BadRequest"));
            Assert.True(await CheckStatusCode(b, "BadRequest"));
            Assert.True(await CheckStatusCode(c, "BadRequest"));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCreateNewRequestCommandsToNotFoundTestFromDataGenerator), MemberType =
            typeof(TestDataGenerator))]
        public async Task CreateNewRequest_ReturnsNotFoundBecauseNoLanguageTarget(CreateRequestCommand a,
            CreateRequestCommand b)
        {
            Assert.True(await CheckStatusCode(a, "NotFound"));
            Assert.True(await CheckStatusCode(b, "NotFound"));
        }

        private async Task<bool> CheckStatusCode(CreateRequestCommand command, string statusCode)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests/",
                content);

            return response.StatusCode.ToString() == statusCode;
        }

        private static Guid TryParseGuidFromString(string str)
        {
            Guid.TryParse(str, out var result);
            Guid.Parse(result.ToString());
            return result;
        }

        [Fact]
        public async Task CreateNewRequest_ReturnsSuccessStatusCode()
        {
            var command = new CreateRequestCommand
            {
                LanguageOriginCode = "en",
                LanguageTargetCode = "dk",
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
                LanguageTargetCode = "dk",
                TextToTranslate = "Bobby har svært ved dansk",
                UserId = Guid.NewGuid()
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests", content);

            response.EnsureSuccessStatusCode();
        }
    }
}