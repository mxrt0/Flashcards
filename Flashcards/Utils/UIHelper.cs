namespace Flashcards.Utils
{
    public static class UIHelper
    {
        public readonly static int NumberOfOptions = 6;
        public static void DisplayOptions()
        {
            Console.WriteLine("\nMAIN MENU\n");
            Console.WriteLine("---------------");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Exit Application");
            Console.WriteLine("\nType 1 to Add New Flashcard");
            Console.WriteLine("\nType 2 to Delete Flashcard");
            Console.WriteLine("\nType 3 to Add New Stack");
            Console.WriteLine("\nType 4 to View Stack");
            Console.WriteLine("\nType 5 to Delete Stack\n");
        }
    }
}
