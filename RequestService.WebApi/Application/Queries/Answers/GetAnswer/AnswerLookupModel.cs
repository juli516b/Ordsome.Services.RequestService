using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Application.Queries.Answers.GetAnswer
{
    public class AnswerLookupModel
    {
        public int Id { get; set; }
        public string TextTranslated { get; set; }
    }
}
