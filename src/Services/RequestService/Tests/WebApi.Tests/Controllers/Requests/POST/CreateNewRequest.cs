using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Commands.Requests.RequestCreation;
using Microsoft.AspNetCore.Mvc.Testing;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class CreateNewRequest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CreateNewRequest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

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
        public async Task CreateNewRequestWithBadLanguage_ReturnsBadRequest()
        {
            var command = new CreateRequestCommand
            {
                LanguageOriginId = 400,
                LanguageTargetId = 600,
                TextToTranslate = "Bobby har svært ved dansk",
                UserId = Guid.NewGuid()
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/requests", content);

            Assert.Equal("NotFound", response.StatusCode.ToString());
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
    }
}