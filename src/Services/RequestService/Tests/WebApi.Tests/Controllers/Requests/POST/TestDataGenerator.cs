using System;
using System.Collections.Generic;
using Application.Commands.Answers.VoteOnAnswer;
using Application.Commands.Requests.AnswerCreation;
using Application.Commands.Requests.RequestCreation;

namespace RequestService.WebApi.Tests.Controllers.Requests.POST
{
    public static class TestDataGenerator
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

        private static Guid TryParseGuidFromString(string str)
        {
            Guid.TryParse(str, out var result);
            Guid.Parse(result.ToString());
            return result;
        }
        
        public static IEnumerable<object[]> GetCreateNewRequestCommandsFromDataGenerator()
        {
            yield return new object[]
            {
                new CreateRequestCommand
                {
                    LanguageOriginCode = "",
                    LanguageTargetCode = "",
                    UserId = TryParseGuidFromString("dafuqqqasd"),
                    TextToTranslate = string.Empty
                },
                new CreateRequestCommand
                {
                    LanguageOriginCode = "dk",
                    LanguageTargetCode = "",
                    UserId = TryParseGuidFromString("dgfsdcasdcsdfeaw"),
                    TextToTranslate = "Meget spændende"
                },
                new CreateRequestCommand
                {
                    LanguageOriginCode = "asdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd",
                    LanguageTargetCode = "asdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd",
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"),
                    TextToTranslate = string.Empty
                },
                new CreateRequestCommand
                {
                    LanguageOriginCode = "-----dasd2111´´",
                    LanguageTargetCode = "ddawdawsdasdd",
                    UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"),
                    TextToTranslate = string.Empty
                },
                new CreateRequestCommand
                {
                LanguageOriginCode = "-----dasd2111´´",
                LanguageTargetCode = "ddawdawsdasdd",
                UserId = TryParseGuidFromString("c"),
                TextToTranslate = "DDDDDDDD                                 DDDDDDDD                 DDDDDD"
                },
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