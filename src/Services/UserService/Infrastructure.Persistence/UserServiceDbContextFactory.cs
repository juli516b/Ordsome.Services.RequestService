using Infrastructure.Persistence.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class UserServiceDbContextFactory : DesignTimeDbContextFactoryBase<UserServiceDbContext>
    {
        protected override UserServiceDbContext CreateNewInstance(DbContextOptions<UserServiceDbContext> options)
        {
            return new UserServiceDbContext(options);
        }
    }
}