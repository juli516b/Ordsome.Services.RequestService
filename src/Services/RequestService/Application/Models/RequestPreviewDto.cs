namespace Application.Models
{
    public class RequestPreviewDto
    {
        public int RequestId { get; set; }
        public string LanguageOriginCode { get; set; }
        public string LanguageTargetCode { get; set; }
        public string TextToTranslate { get; set; }
        public int NoOfAnswers { get; set; }
        public bool IsClosed { get; set; }
    }
}