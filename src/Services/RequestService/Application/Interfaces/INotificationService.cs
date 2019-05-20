using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}