using RequestService.Application.Models;
using System.Threading.Tasks;

namespace RequestService.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
