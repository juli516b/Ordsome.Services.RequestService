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
                new AnswerVote { AnswerId = 1, UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"), Like = true},
                new AnswerVote { AnswerId = 2, UserId = Guid.Parse("b8f632a1-ef73-4ef1-9582-87850f1b7e91"), Like = true},
                new AnswerVote { AnswerId = 3, UserId = Guid.Parse("1e48921b-9736-47df-8863-9c8395c74bab"), Like = true}
            };

            context.AnswerVotes.AddRange(answervotes);

            context.SaveChanges();
        }

        public void SeedRequestsWithAnswers(RequestServiceDbContext context)
        {
            var answersForRequest1 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 1,TextTranslated = "Det er en ko", UserId = Guid.Parse("b8f632a1-ef73-4ef1-9582-87850f1b7e91")},
                new Answer { IsPreferred = false, RequestId = 1,TextTranslated = "Det en ko", UserId = Guid.Parse("1e48921b-9736-47df-8863-9c8395c74bab")},
            };

            var answersForRequest2 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 2,TextTranslated = "Det er en mand", UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f")},
                new Answer { IsPreferred = false, RequestId = 2,TextTranslated = "Det en mand", UserId = Guid.Parse("1e48921b-9736-47df-8863-9c8395c74bab")},
            };

            var answersForRequest3 = new[]
            {
                new Answer { IsPreferred = false, RequestId = 3,TextTranslated = "Det er en kat!", UserId = Guid.Parse("bab8a434-4892-4834-949c-3a603b49b426")},
                new Answer { IsPreferred = false, RequestId = 3,TextTranslated = "Det en kat", UserId = Guid.Parse("8531802a-95e0-4287-8edf-16f7fecc3204")},
            };

            var requests = new[]
            {
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a cow", UserId = Guid.Parse("cb23e0db-208f-421d-9210-4b976576056f"), Answers = answersForRequest1},
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a man!", UserId = Guid.Parse("b8f632a1-ef73-4ef1-9582-87850f1b7e91"), Answers = answersForRequest2  },
                new Request { LanguageOrigin = "en", LanguageTarget="dk", IsClosed = false, TextToTranslate="Its a cat", UserId = Guid.Parse("1e48921b-9736-47df-8863-9c8395c74bab"), Answers = answersForRequest3  }
            };
            context.Requests.AddRange(requests);

            context.SaveChanges();
        }
    }
}
