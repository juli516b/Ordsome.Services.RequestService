using RequestService.WebApi.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
