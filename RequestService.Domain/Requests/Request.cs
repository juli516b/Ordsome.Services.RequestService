using RequestService.Domain.SharedKernel;
using System.Collections.Generic;

namespace RequestService.Domain.Requests
{
    public class Request : BaseEntity
    {
        public Request()
        {
            Answers = new HashSet<Answer>();
        }

        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
