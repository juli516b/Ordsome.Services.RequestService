using System;
using System.Threading.Tasks;
using RestEase;

namespace Application.RestClients
{
    public interface IRequestServiceClient
    {
        [Path("userId")] Guid UserId { get; set; }

        [Get("u/{userId}")]
        Task<string> GetRequests([Path] Guid userId);

        [Get("u/{userId}")]
        Task<string> GetAnswers([Path] Guid userId);
    }
}