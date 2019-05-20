using System;
using System.Threading.Tasks;
using RestEase;

namespace Application.RestClients
{
    public interface IUserServiceClient
    {
        [Path("userId")] Guid UserId { get; set; }

        [Get("check/{userId}")]
        Task<bool> CheckUserId([Path] Guid userId);


        [Get("{userId}")]
        Task<UserDto> GetUserDetails([Path] Guid userId);
    }
}