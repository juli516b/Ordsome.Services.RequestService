namespace Application.Queries.Requests.GetRequests
{
    public class RequestPreviewDto
    {
        //TODO - move this to a shared models folder
        public int RequestId { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public int NoOfAnswers { get; set; }
        public bool IsClosed { get; set; }
    }
}