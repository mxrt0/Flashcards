namespace Flashcards.Utils
{
    public static class UIHelper
    {
        public readonly static int NumberOfOptions = 12;
        public static void DisplayOptions()
        {
            Console.WriteLine("\n\nMAIN MENU\n");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Exit Application");
            Console.WriteLine("\nType 1 to Add New Flashcard");
            Console.WriteLine("\nType 2 to Delete Flashcard From Stack");
            Console.WriteLine("\nType 3 to Add New Stack");
            Console.WriteLine("\nType 4 to View Flashcards In Stack");
            Console.WriteLine("\nType 5 to Delete Stack");
            Console.WriteLine("\nType 6 to Start New Study Session");
            Console.WriteLine("\nType 7 to Edit Existing Flashcard");
            Console.WriteLine("\nType 8 to Edit Existing Stack");
            Console.WriteLine("\nType 9 to View All Study Sessions");
            Console.WriteLine("\nType 10 to View Monthly Sessions Count By Year");
            Console.WriteLine("\nType 11 to View Monthly Average Session Score By Year\n");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
        }
    }
}
