using System;

namespace RequestService.Domain.Requests
{
    public class AnswerVote
    {
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
        public Guid UserId { get; set; }
        public bool Like { get; set; }
    }
}