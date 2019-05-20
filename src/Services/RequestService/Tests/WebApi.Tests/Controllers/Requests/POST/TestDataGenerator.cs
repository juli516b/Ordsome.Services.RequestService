using System;
using System.Collections.Generic;
using Application.Commands.Answers.AnswerCreation;

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
    }
}