using System;
using System.Collections.Generic;
using Domain.SharedKernel;
using RequestService.Domain.SharedKernel;

namespace RequestService.Domain.Requests
{
    public class Request : BaseEntity, IAggregateRoot
    {
        public Request()
        {
            Answers = new HashSet<Answer>();
        }
        public Guid UserId { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}