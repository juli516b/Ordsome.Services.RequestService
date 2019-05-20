using Infrastructure.Persistence.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class RequestServiceDbContextFactory : DesignTimeDbContextFactoryBase<RequestServiceDbContext>
    {
        protected override RequestServiceDbContext CreateNewInstance(DbContextOptions<RequestServiceDbContext> options)
        {
            return new RequestServiceDbContext(options);
        }
    }
}