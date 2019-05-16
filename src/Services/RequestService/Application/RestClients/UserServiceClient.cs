using System;
using System.Net.Http;
using System.Threading.Tasks;
using RestEase;

namespace RequestService.Application.RestClients
{
    public class UserServiceClient : IUserServiceClient
    {
        public Guid UserId { get; set; }

        public async Task<bool> CheckUserId(Guid userId)
        {
            IUserServiceClient api = RestClient.For<IUserServiceClient>("http://localhost:7002/api/users");
            bool boolToReturn = await api.CheckUserId(userId);
            return boolToReturn;
        }
    }
}