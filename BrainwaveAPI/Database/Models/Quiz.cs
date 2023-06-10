namespace BrainwaveAPI.Database.Models
{
    public class Quiz : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Visible { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
