using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Users;

namespace Application.Interfaces
{
    public interface IUserServiceDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
