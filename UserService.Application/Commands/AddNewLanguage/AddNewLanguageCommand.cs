using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
}