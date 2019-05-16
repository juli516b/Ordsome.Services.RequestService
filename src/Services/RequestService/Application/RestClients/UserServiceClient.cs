using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.RestClients;
using Newtonsoft.Json;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RestEase;

namespace RequestService.Application.RestClients
{
    public class UserServiceClient : IUserServiceClient
    {
        public Guid UserId { get; set; }

        public async Task<bool> CheckUserId(Guid userId)
        {
            IUserServiceClient api = RestClient.For<IUserServiceClient>("http://localhost:7002/api/users");
            return await api.CheckUserId(userId);
        }

        public async Task<UserDto> GetUserDetails([Path] Guid userId)
        {
            IUserServiceClient api = RestClient.For<IUserServiceClient>("http://localhost:7002/api/users");
            return await api.GetUserDetails(userId);
        }
    }
}