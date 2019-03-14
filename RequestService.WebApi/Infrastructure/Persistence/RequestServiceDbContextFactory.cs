using Microsoft.EntityFrameworkCore;
using RequestService.WebApi.Infrastructure.Persistence.SharedKernel;

namespace RequestService.WebApi.Infrastructure.Persistence
{
    public class RequestServiceDbContextFactory : DesignTimeDbContextFactoryBase<RequestServiceDbContext>
    {
        protected override RequestServiceDbContext CreateNewInstance(DbContextOptions<RequestServiceDbContext> options)
        {
            return new RequestServiceDbContext(options);
        }
    }
}
