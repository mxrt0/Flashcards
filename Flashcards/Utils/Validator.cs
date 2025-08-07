namespace Flashcards.Utils
{
    public static class Validator
    {
        public static bool IsUserInputValid(string? input)
        {
            return !string.IsNullOrEmpty(input) && int.TryParse(input, out _)
                && int.Parse(input) > 0 && int.Parse(input) <= UIHelper.NumberOfOptions;
        }
        public static bool IsStackNameValid(string? itemName)
        {
            return !string.IsNullOrEmpty(itemName) && itemName.Length <= 150;
        }
        public static bool IsFlashcardTextValid(string? text)
        {
            return !string.IsNullOrEmpty(text) && text.Length <= 255;
        }
        public static bool IsYearValid(string? year = "")
        {
            return !string.IsNullOrEmpty(year) && int.TryParse(year, out _) && int.Parse(year) >= 1;
        }
    }
}
