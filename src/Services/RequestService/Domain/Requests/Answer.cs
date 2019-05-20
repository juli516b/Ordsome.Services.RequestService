using System;
using Domain.SharedKernel;

namespace Domain.Requests
{
    public class Answer : BaseEntity, IAggregateRoot
    {
        public string TextTranslated { get; set; }
        public Request Request { get; set; }
        public int RequestId { get; set; }
        public bool IsPreferred { get; set; }
        public Guid UserId { get; set; }
    }
}