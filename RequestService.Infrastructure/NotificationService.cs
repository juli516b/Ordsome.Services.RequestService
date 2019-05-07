using System.Threading.Tasks;
using RequestService.Application.Interfaces;
using RequestService.Application.Models;

namespace RequestService.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync (Message message)
        {
            return Task.CompletedTask;
        }
    }
}