using System.Threading;
using System.Threading.Tasks;
using Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IRequestServiceDbContext
    {
        DbSet<Answer> Answers { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<AnswerVote> AnswerVotes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}