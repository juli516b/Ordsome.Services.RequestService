using System;
using RequestService.Domain.SharedKernel;

namespace RequestService.Domain.Requests
{
    public class Answer : BaseEntity
    {
        public string TextTranslated { get; set; }
        public Request Request { get; set; }
        public int RequestId { get; set; }
        public bool IsPreferred { get; set; }
        public Guid UserId { get; set; }
    }
}