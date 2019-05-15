using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence
{
    public class OrdsomeInitializer
    {
        private readonly Dictionary<int, Request> Requests = new Dictionary<int, Request>();
        private readonly Dictionary<int, AnswerVote> AnswerVotes = new Dictionary<int, AnswerVote>();
        private readonly Dictionary<int, Answer> Answers = new Dictionary<int, Answer>();

        public static void Initialize(RequestServiceDbContext context)
        {
            var initializer = new OrdsomeInitializer();
            initializer.SeedEverything(context);
        }

        private void SeedEverything(RequestServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Requests.Any())
            {
                return;
            }
            SeedRequestsWithAnswers(context);
            SeedAnswerVotes(context);
        }

        public void SeedAnswerVotes(RequestServiceDbContext context)
        {
            var answervotes = new[]
            {
                new AnswerVote { AnswerId = 1, UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426"), Like = true},
                new AnswerVote { AnswerId = 12, UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426"), Like = true},
                new AnswerVote { AnswerId = 8, UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426"), Like = true}
            };

            context.AnswerVotes.AddRange(answervotes);

            context.SaveChanges();
        }

        public void SeedRequestsWithAnswers(RequestServiceDbContext context)
        {
            var answersForRequest1 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 1,TextTranslated = "Det er en ko", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 1,TextTranslated = "Det en ko", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var answersForRequest2 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 2,TextTranslated = "Det er en mand", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 2,TextTranslated = "Det en mand", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var answersForRequest3 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 3,TextTranslated = "Det er en kat!", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 3,TextTranslated = "Det en kat", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var answersForRequest4 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 4,TextTranslated = "Det er et stort hus", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 4,TextTranslated = "Det er stort det hus", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var answersForRequest5 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 5,TextTranslated = "Det er noget af en kriger", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 5,TextTranslated = "For satan en kriger", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var answersForRequest6 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 6,TextTranslated = "Vinteren kommer", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 6,TextTranslated = "Vinteren er på vej", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var requests = new[]
            {
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a cow", UserId = Guid.Parse("46666844-8d47-4163-bc54-b763e6a2fea3"), Answers = answersForRequest1},
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a man!", UserId = Guid.Parse("46666844-8d47-4163-bc54-b763e6a2fea3"), Answers = answersForRequest2  },
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a cat", UserId = Guid.Parse("46666844-8d47-4163-bc54-b763e6a2fea3"), Answers = answersForRequest3  },
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Thats a big house", UserId = Guid.Parse("f5868e20-f943-4027-9cf9-a222df814d1f"), Answers = answersForRequest4  },
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Thats one hell of a warrior", UserId = Guid.Parse("f5868e20-f943-4027-9cf9-a222df814d1f"), Answers = answersForRequest5  },
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Winter is coming", UserId = Guid.Parse("f5868e20-f943-4027-9cf9-a222df814d1f"), Answers = answersForRequest6  }
            };
            context.Requests.AddRange(requests);

            context.SaveChanges();
        }
    }
}
