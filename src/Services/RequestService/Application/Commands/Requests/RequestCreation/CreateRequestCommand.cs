using System;
using MediatR;

namespace Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommand : INotification
    {
        public int LanguageOriginId { get; set; }
        public int LanguageTargetId { get; set; }
        public string TextToTranslate { get; set; }
        public Guid UserId { get; set; }
    }
}