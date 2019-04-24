using RequestService.Application.Interfaces;
using RequestService.Application.Models;
using System.Threading.Tasks;

namespace RequestService.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}