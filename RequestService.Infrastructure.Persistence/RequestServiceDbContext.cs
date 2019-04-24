using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;

namespace RequestService.Infrastructure.Persistence
{
    public class RequestServiceDbContext : DbContext
    {
        public RequestServiceDbContext(DbContextOptions<RequestServiceDbContext> options)
         : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RequestServiceDbContext).Assembly);
        }
    }
}
