using System;
using System.Linq;
using Domain.Requests;

namespace Infrastructure.Persistence
{
    public class OrdsomeInitializer
    {
        public static void Initialize(RequestServiceDbContext context)
        {
            SeedEverything(context);
        }

        private static void SeedEverything(RequestServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Requests.Any()) return;
            SeedRequestsWithAnswers(context);
            SeedAnswerVotes(context);
        }

        private static void SeedAnswerVotes(RequestServiceDbContext context)
        {
            var answervotes = new[]
            {
                new AnswerVote {AnswerId = 1, UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76"), Like = true},
                new AnswerVote {AnswerId = 2, UserId = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7"), Like = true},
                new AnswerVote {AnswerId = 3, UserId = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b"), Like = true}
            };

            context.AnswerVotes.AddRange(answervotes);

            context.SaveChanges();
        }

        private static void SeedRequestsWithAnswers(RequestServiceDbContext context)
        {
            var answersForRequest1 = new[]
            {
                new Answer
                {
                    IsPreferred = false, RequestId = 1, TextTranslated = "Det er en ko",
                    UserId = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7")
                },
                new Answer
                {
                    IsPreferred = false, RequestId = 1, TextTranslated = "Det en ko",
                    UserId = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b")
                }
            };

            var answersForRequest2 = new[]
            {
                new Answer
                {
                    IsPreferred = false, RequestId = 2, TextTranslated = "Det er en mand",
                    UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76")
                },
                new Answer
                {
                    IsPreferred = false, RequestId = 2, TextTranslated = "Det en mand",
                    UserId = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b")
                }
            };

            var answersForRequest3 = new[]
            {
                new Answer
                {
                    IsPreferred = false, RequestId = 3, TextTranslated = "Det er en kat!",
                    UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76")
                },
                new Answer
                {
                    IsPreferred = false, RequestId = 3, TextTranslated = "Det en kat",
                    UserId = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7")
                }
            };

            var requests = new[]
            {
                new Request
                {
                    LanguageOrigin = "en", LanguageTarget = "dk", IsClosed = false, TextToTranslate = "Its a cow",
                    UserId = Guid.Parse("8e3f52d0-ee7e-4353-8941-ab1b75bbdf76"), Answers = answersForRequest1
                },
                new Request
                {
                    LanguageOrigin = "en", LanguageTarget = "dk", IsClosed = false, TextToTranslate = "Its a man!",
                    UserId = Guid.Parse("50c41b86-1620-4912-9b5d-561b34dc27f7"), Answers = answersForRequest2
                },
                new Request
                {
                    LanguageOrigin = "en", LanguageTarget = "dk", IsClosed = false, TextToTranslate = "Its a cat",
                    UserId = Guid.Parse("0e8e8abd-0f47-4164-9a1d-55ed125db66b"), Answers = answersForRequest3
                }
            };
            context.Requests.AddRange(requests);

            context.SaveChanges();
        }
    }
}