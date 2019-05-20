using System;
using MediatR;

namespace Application.Commands.Answers.VoteOnAnswer
{
    public class VoteOnAnswerCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public bool Like { get; set; }
    }
}