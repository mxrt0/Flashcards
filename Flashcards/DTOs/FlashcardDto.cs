using System.Text;

namespace Flashcards.DTOs
{
    public class FlashcardDto
    {
        public int DisplayId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n{");
            sb.AppendLine($"    Flashcard ID: {DisplayId}");
            sb.AppendLine($"    Question: {Front}");
            sb.AppendLine($"    Answer: {Back}");
            sb.AppendLine("}\n");
            return sb.ToString().TrimEnd();
        }
    }
}
