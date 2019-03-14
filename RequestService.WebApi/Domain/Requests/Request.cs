using RequestService.WebApi.Domain.Events.Requests;
using RequestService.WebApi.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
