namespace BrainwaveAPI.Database.Models
{
    public class Question
    {
        public long Id { get; set; }
        public long QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = default!;
        public string Text { get; set; } = string.Empty;
        public byte[] Image { get; set; } = Array.Empty<byte>();

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
