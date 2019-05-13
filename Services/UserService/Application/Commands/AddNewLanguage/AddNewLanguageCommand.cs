using System;
using MediatR;

namespace UserService.Application.Commands.AddNewLanguage
{
    public class AddNewLanguageCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
}