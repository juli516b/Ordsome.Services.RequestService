namespace Application.Queries.GetAnswersBasedOnUserId
{
    public class UserAnswersDto
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public bool IsPreferred { get; set; }
    }
}