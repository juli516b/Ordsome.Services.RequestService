namespace Application.Models
{
    public class AnswerDto
    {
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public string TextTranslated { get; set; }
        public string TextToTranslate { get; set; }
        public bool IsPreferred { get; set; }
        public int AmountOfLikes { get; set; }
    }
}