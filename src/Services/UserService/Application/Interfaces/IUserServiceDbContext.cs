using System.Threading;
using System.Threading.Tasks;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IUserServiceDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}