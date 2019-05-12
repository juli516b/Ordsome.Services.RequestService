using System;
using System.Net.Http;
using System.Threading.Tasks;
using RestEase;

namespace RequestService.Application.RestClients
{
    public interface IUserServiceClient
    {
        [Path("userId")]
        Guid UserId { get; set; }

        [Get("{userId}")]
        Task<bool> CheckUserId();
    }
}