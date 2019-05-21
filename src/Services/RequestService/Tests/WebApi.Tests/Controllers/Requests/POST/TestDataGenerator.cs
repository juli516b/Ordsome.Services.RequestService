using System;
using System.Collections.Generic;
using Application.Commands.Answers.VoteOnAnswer;
using Application.Commands.Requests.AnswerCreation;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetCreateAnswerCommandsFromDataGenerator()
        {
            yield return new object[]
            {
                new CreateAnswerCommand
                {
                    TextTranslated = "",
                    RequestId = 1,
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f")
                },
                new CreateAnswerCommand
                {
                    TextTranslated = "Dette skal oversættes",
                    RequestId = 0,
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f")
                },
                new CreateAnswerCommand
                {
                    TextTranslated = "Dette skal oversættes",
                    RequestId = 1,
                    UserId = Guid.NewGuid()
                }
            };
        }

        public static IEnumerable<object[]> GetCreateAnswerVoteCommandsFromDataGenerator()
        {
            yield return new object[]
            {
                new VoteOnAnswerCommand
                {
                    AnswerId = 0,
                    RequestId = 1,
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"),
                    Like = false
                },
                new VoteOnAnswerCommand
                {
                    AnswerId = 1,
                    RequestId = 1000000,
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"),
                    Like = false
                },
                new VoteOnAnswerCommand
                {
                    AnswerId = 1,
                    RequestId = 1,
                    UserId = Guid.NewGuid(),
                    Like = false
                },
                new VoteOnAnswerCommand
                {
                    AnswerId = 0,
                    RequestId = 0,
                    UserId = Guid.NewGuid(),
                    Like = false
                },
                new VoteOnAnswerCommand
                {
                    AnswerId = int.MaxValue,
                    RequestId = int.MaxValue,
                    UserId = Guid.NewGuid(),
                    Like = false
                }
            };
        }
    }
}