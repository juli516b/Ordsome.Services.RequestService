using System;
using System.Threading.Tasks;
using RestEase;

namespace Application.RestClients
{
    public class UserServiceClient : IUserServiceClient
    {
        public Guid UserId { get; set; }

        public async Task<bool> CheckUserId(Guid userId)
        {
            var api = RestClient.For<IUserServiceClient>("http://localhost:7002/api/users");
            return await api.CheckUserId(userId);
        }

        public async Task<UserDto> GetUserDetails([Path] Guid userId)
        {
            var api = RestClient.For<IUserServiceClient>("http://localhost:7002/api/users");
            return await api.GetUserDetails(userId);
        }

        //TODO - need a getuserlanguages.
    }
}