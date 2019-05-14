using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
