using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Commands.Answers.VoteOnAnswer;
using RequestService.WebApi.Tests.Common;
using WebApi;
using Xunit;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class CreateAnswerVote : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CreateAnswerVote(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private readonly HttpClient _client;

        private async Task<bool> IsBadRequest(VoteOnAnswerCommand command)
        {
            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers/{command.AnswerId}/vote",
                content);

            return response.StatusCode.ToString() == "BadRequest";
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCreateAnswerVoteCommandsFromDataGenerator), MemberType =
            typeof(TestDataGenerator))]
        public async Task CreateAnswerVote_EnsureBadRequest(VoteOnAnswerCommand a, VoteOnAnswerCommand b,
            VoteOnAnswerCommand c, VoteOnAnswerCommand d, VoteOnAnswerCommand e)
        {
            Assert.True(await IsBadRequest(a));
            Assert.True(await IsBadRequest(b));
            Assert.True(await IsBadRequest(c));
            Assert.True(await IsBadRequest(d));
            Assert.True(await IsBadRequest(e));
        }

        [Fact]
        public async Task CreateAnswerVote_EnsureForbiddenRequest()
        {
            var command = new VoteOnAnswerCommand
            {
                RequestId = 1,
                AnswerId = 2,
                UserId = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b"),
                Like = true
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers/{command.AnswerId}/vote",
                content);

            var responseStatusCode = response.StatusCode.ToString();

            Assert.Equal("Forbidden", responseStatusCode);
        }

        [Fact]
        public async Task CreateAnswerVote_EnsureSuccessStatusCode()
        {
            var command = new VoteOnAnswerCommand
            {
                RequestId = 1,
                AnswerId = 2,
                UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76"),
                Like = true
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/requests/{command.RequestId}/answers/{command.AnswerId}/vote",
                content);

            var responseStatusCode = response.StatusCode.ToString();

            Assert.Equal("NoContent", responseStatusCode);
        }
    }
}