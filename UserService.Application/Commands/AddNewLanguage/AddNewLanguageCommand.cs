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
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string LanguageNativeName { get; set; }
    }
}