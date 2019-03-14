using RequestService.WebApi.Application.Interfaces;
using RequestService.WebApi.Application.Models;
using System.Threading.Tasks;

namespace RequestService.WebApi.Infrastructure.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}