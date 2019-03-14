using RequestService.WebApi.Domain.SharedKernel;

namespace RequestService.WebApi.Domain.Requests
{
    public class Answer : BaseEntity
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
    }
}