using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Commands.Answers.AnswerCreation;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class CreateNewAnswer : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CreateNewAnswer(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        private async Task<bool> IsBadRequest(CreateAnswerCommand command)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers", content);

            return response.StatusCode.ToString() == "BadRequest";
        }

        public async Task<bool> IsNotFound(CreateAnswerCommand command)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers", content);

            return response.StatusCode.ToString() == "NotFound";
        }

        private async Task<bool> IsSuccessStatusCode(CreateAnswerCommand command)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers", content);

            return response.StatusCode.ToString() == "NoContent";
        }


        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCreateAnswerCommandsFromDataGenerator), MemberType =
            typeof(TestDataGenerator))]
        public async void CreateNewAnswer_ReturnsBadRequest(CreateAnswerCommand a, CreateAnswerCommand b,
            CreateAnswerCommand c)
        {
            Assert.True(await IsBadRequest(a));
            Assert.True(await IsBadRequest(b));
            Assert.True(await IsBadRequest(c));
        }

        [Fact]
        public async Task CreateNewAnswer_ReturnsSuccessStatusCode()
        {
            //Arrange
            var command = new CreateAnswerCommand
            {
                TextTranslated = "Den er nu fikset",
                RequestId = 1,
                UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76")
            };

            Assert.True(await IsSuccessStatusCode(command));
        }
    }
}