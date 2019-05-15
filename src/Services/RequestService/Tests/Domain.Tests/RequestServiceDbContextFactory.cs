using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;
using RequestService.Infrastructure.Persistence;
using System;

namespace Application.Tests
{
    public class RequestServiceDbContextFactory
    {
        public static RequestServiceDbContext Create()
        {
            var options = new DbContextOptionsBuilder<RequestServiceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new RequestServiceDbContext(options);

            context.Database.EnsureCreated();

            context.Answers.AddRange(new[]
            {
                new Answer { }
            });

            return null;
        }
    }
}
