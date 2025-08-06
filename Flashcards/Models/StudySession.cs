namespace Flashcards.Models
{
    public class StudySession
    {
        public int Id { get; set; }
        public required DateTime SessionDate { get; set; }
        public int Score { get; set; }
    }
}
