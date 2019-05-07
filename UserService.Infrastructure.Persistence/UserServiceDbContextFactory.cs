using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence.SharedKernel;

namespace UserService.Infrastructure.Persistence
{
    public class UserServiceDbContextFactory : DesignTimeDbContextFactoryBase<UserServiceDbContext>
    {
        protected override UserServiceDbContext CreateNewInstance(DbContextOptions<UserServiceDbContext> options)
        {
            return new UserServiceDbContext(options);
        }
    }
}
