using System.Text;

namespace Flashcards.DTOs
{
    public class MonthlyAverageScoreReport
    {
        public string StackName { get; set; }
        public double January { get; set; }
        public double February { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("- - - - - - - - - - - - - - - -");
            sb.AppendLine($"Stack: {StackName}");
            sb.AppendLine($"January: {January}");
            sb.AppendLine($"February: {February}");
            sb.AppendLine($"March: {March}");
            sb.AppendLine($"April: {April}");
            sb.AppendLine($"May: {May}");
            sb.AppendLine($"June: {June}");
            sb.AppendLine($"July: {July}");
            sb.AppendLine($"August: {August}");
            sb.AppendLine($"September: {September}");
            sb.AppendLine($"October: {October}");
            sb.AppendLine($"November: {November}");
            sb.AppendLine($"December: {December}");
            sb.AppendLine("- - - - - - - - - - - - - - - -");
            return sb.ToString().TrimEnd();
        }
    }
}
