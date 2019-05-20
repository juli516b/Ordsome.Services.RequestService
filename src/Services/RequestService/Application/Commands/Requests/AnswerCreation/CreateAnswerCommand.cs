using System;
using MediatR;

namespace Application.Commands.Requests.AnswerCreation
{
    public class CreateAnswerCommand : IRequest<AnswerIdDto>
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
        public Guid UserId { get; set; }
    }
}