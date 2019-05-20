using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}