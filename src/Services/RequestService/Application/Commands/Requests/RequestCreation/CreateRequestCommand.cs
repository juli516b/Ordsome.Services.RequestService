using System;
using MediatR;

namespace Application.Commands.Requests.RequestCreation
{
    public class CreateRequestCommand : INotification
    {
        public string LanguageOriginCode { get; set; }
        public string LanguageTargetCode { get; set; }
        public string TextToTranslate { get; set; }
        public Guid UserId { get; set; }
    }
}