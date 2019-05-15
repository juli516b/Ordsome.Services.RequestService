using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestEase;

namespace UserService.Application.RestClients
{
    public class RequestServiceClient : IRequestServiceClient
    {
        public Guid UserId { get; set; }

        public async Task<string> GetAnswers([Path] Guid userId)
        {
            IRequestServiceClient api = RestClient.For<IRequestServiceClient>("http://localhost:7001/api/requests");
            return await api.GetAnswers(userId);
        }

        public async Task<string> GetRequests([Path] Guid userId)
        {
            IRequestServiceClient api = RestClient.For<IRequestServiceClient>("http://localhost:7001/api/answers");
            return await api.GetRequests(userId);
        }
    }
}