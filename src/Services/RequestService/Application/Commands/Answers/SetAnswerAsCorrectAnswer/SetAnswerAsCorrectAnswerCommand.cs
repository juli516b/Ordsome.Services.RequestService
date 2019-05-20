using MediatR;

namespace Application.Commands.Answers.SetAnswerAsCorrectAnswer
{
    public class SetAnswerAsCorrectAnswerCommand : INotification
    {
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public bool IsPreferred { get; set; }
    }
}