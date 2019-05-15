using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public class AnswerDto
    {
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public string TextTranslated { get; set; }
        public bool IsPreferred { get; set; }
    }
}