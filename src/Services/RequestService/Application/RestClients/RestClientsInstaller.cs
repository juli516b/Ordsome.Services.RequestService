using Microsoft.Extensions.DependencyInjection;

namespace Application.RestClients
{
    public static class RestClientsInstaller
    {
        public static void AddRestServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IUserServiceClient), typeof(UserServiceClient));
        }
    }
}