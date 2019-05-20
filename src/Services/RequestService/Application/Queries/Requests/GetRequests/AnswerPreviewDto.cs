namespace Application.Queries.Requests.GetRequests
{
    //TODO - move this to a shared models folder
    public class AnswerPreviewDto
    {
        public int AnswerId { get; set; }
        public string TextTranslated { get; set; }
    }
}