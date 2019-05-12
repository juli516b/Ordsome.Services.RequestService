using Microsoft.Extensions.DependencyInjection;

namespace UserService.Application.RestClients
{
    public static class RestClientsInstaller
    {
        public static IServiceCollection AddRestServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRequestServiceClient), typeof(RequestServiceClient));
            return services;
        }
    }
}
