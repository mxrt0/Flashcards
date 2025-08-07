using System.Text;

namespace Flashcards.DTOs
{
    public class MonthlySessionReport
    {
        public string StackName { get; set; }
        public int January { get; set; }
        public int February { get; set; }
        public int March { get; set; }
        public int April { get; set; }
        public int May { get; set; }
        public int June { get; set; }
        public int July { get; set; }
        public int August { get; set; }
        public int September { get; set; }
        public int October { get; set; }
        public int November { get; set; }
        public int December { get; set; }

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
