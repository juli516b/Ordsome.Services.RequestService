using System;
using Domain.SharedKernel;

namespace Domain.Requests
{
    public class AnswerVote : BaseEntity, IAggregateRoot
    {
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
        public Guid UserId { get; set; }
        public bool Like { get; set; }
    }
}