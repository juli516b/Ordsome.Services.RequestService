namespace Application.Queries.GetRequestsBasedOnUserId
{
    public class UserRequestsDto
    {
        public int RequestID { get; set; }
        public string LanguageOriginCode { get; set; }
        public string LanguageTargetCode { get; set; }
        public string TextToTranslate { get; set; }
        public bool IsClosed { get; set; }
        public int NoOfAnswers { get; set; }
    }
}