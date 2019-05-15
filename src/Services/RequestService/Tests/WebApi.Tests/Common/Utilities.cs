using Infrastructure.Persistence;
using Newtonsoft.Json;
using RequestService.Infrastructure.Persistence;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public static void InitializeDbForTests(RequestServiceDbContext context)
        {
            OrdsomeInitializer.Initialize(context);
        }
    }
}
