using RequestService.WebApi.Domain.SharedKernel;
using System.Collections.Generic;

namespace RequestService.WebApi.Domain.Requests
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
        public ICollection<Answer> Answers { get; set; }
    }
}
