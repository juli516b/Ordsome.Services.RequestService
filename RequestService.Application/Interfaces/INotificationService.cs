using System.Threading.Tasks;
using RequestService.Application.Models;

namespace RequestService.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync (Message message);
    }
}