using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;


namespace RequestService.Infrastructure.Persistence
{
    public class RequestServiceDbContext : DbContext, IRequestServiceDbContext
    {
        public RequestServiceDbContext(DbContextOptions<RequestServiceDbContext> options) : base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RequestServiceDbContext).Assembly);
        }
    }
}