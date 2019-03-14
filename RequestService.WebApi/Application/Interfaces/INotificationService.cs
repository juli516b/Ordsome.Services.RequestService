using RequestService.WebApi.Application.Models;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
