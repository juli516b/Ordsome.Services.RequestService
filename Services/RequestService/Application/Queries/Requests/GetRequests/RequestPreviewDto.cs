using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RequestService.Domain.Requests;

namespace RequestService.Application.Queries.Requests.GetRequests
{
    public class RequestPreviewDto
    {
        public int RequestId { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public int NoOfAnswers { get; set; }
        public bool IsClosed { get; set; }
        public Guid UserId { get; set; }
    }
}