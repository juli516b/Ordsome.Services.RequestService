using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RequestService.Application.Commands.Answers.SetAnswerAsCorrectAnswer
{
    public class SetAnswerAsCorrectAnswerCommand : INotification
    {
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public bool IsPreferred { get; set; }
    }
}