namespace BrainwaveAPI.Database.Models
{
    public class Answer
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public virtual Question Question { get; set; } = default!;
        public string Text { get; set; } = string.Empty;
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public bool IsCorrect { get; set; }
    }
}
