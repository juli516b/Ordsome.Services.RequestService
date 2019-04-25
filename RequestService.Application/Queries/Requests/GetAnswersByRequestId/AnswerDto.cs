using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Application.Queries.Requests.GetAnswersByRequestId
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }
        public string TextTranslated { get; set; }
    }
}
