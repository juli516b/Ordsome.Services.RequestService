using System;
using System.Threading.Tasks;
using RestEase;

namespace RequestService.Application.RestClients
{
    public interface IUserServiceClient
    {
        [Path("userId")]
        Guid UserId { get; set; }

        [Get("{userId}")]
        Task<bool> CheckUserId([Path] Guid userId);
    }
}