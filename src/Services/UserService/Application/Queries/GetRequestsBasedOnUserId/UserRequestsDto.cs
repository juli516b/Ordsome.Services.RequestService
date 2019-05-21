namespace Application.Queries.GetRequestsBasedOnUserId
{
    public class UserRequestsDto
    {
        public int RequestID { get; set; }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public bool IsClosed { get; set; }
        public int NoOfAnswers { get; set; }
    }
}