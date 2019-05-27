using System;
using System.Threading.Tasks;
using RestEase;

namespace Application.RestClients
{
    public class RequestServiceClient : IRequestServiceClient
    {
        public Guid UserId { get; set; }

        public async Task<string> GetAnswers([Path] Guid userId)
        {
            var api = RestClient.For<IRequestServiceClient>("http://localhost:7001/api/answers/");
            return await api.GetAnswers(userId);
        }

        public async Task<string> GetRequests([Path] Guid userId)
        {
            var api = RestClient.For<IRequestServiceClient>("http://localhost:7001/api/requests/");
            return await api.GetRequests(userId);
        }
    }
}