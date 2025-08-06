using System.Text;

namespace Flashcards.Models
{
    public class StudySession
    {
        public int SessionId { get; set; }
        public required DateTime SessionDate { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n- - - - - - - - - - - - - - -\n");
            sb.AppendLine($"Session ID: {SessionId}");
            sb.AppendLine($"Date: {SessionDate.ToString("dd-MM-yyyy")}");
            sb.AppendLine($"Score: {Score}");
            sb.AppendLine("\n- - - - - - - - - - - - - - -\n");
            return sb.ToString().TrimEnd();
        }
    }
}
