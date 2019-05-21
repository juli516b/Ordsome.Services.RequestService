using System;
using Domain.Requests;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RequestService.Application.Tests
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

            context.Answers.AddRange(new Answer());

            return null;
        }
    }
}