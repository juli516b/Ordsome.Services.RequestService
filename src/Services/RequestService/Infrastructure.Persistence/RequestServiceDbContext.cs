using Application.Interfaces;
using Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class RequestServiceDbContext : DbContext, IRequestServiceDbContext
    {
        public RequestServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RequestServiceDbContext).Assembly);
        }
    }
}