namespace Flashcards.Utils
{
    public static class Messages
    {
        // Main Menu 
        public readonly static string ReturnToMainMenuMessage = "You can type 0 to return to the Main Menu:\n";

        // User Input

        public readonly static string InvalidInputMessage = $"\nInvalid input. Please enter a number between 0 and {UIHelper.NumberOfOptions}\n";


        // Flashcard

        public readonly static string FlashcardStackParentPrompt = "\nPlease enter the name of the stack your flash card will be stored in (if it does not exist yet, you must first create it):\n";

        public readonly static string FlashcardToDeleteStackParentPrompt = "\nPlease enter the exact name of the stack the flashcard you wish to delete belongs to (case-insensitive):\n";

        public readonly static string FlashcardQuestionPrompt = "\nPlease enter the question on your flash card's front side (up to 255 characters):\n ";

        public readonly static string FlashcardToDeleteQuestionPrompt = "\nPlease enter the question on the front side of the flashcard you wish to delete:\n ";

        public readonly static string FlashcardBackPrompt = "\nPlease enter the answer on your flash card's back side (up to 255 characters):\n ";

        public readonly static string InvalidFlashcardTextMessage = "\nInvalid flashcard text. Please enter a non-null string of length equal to or less than 255 characters.\n";

        public readonly static string SuccessfullyAddedFlashcardMessage = "\nSuccessfully added flashcard to stack '{0}'!\n";

        public readonly static string FlashcardAlreadyExists = "\nA flashcard with this question already exists in the stack you have selected. Please try again!\n";

        public readonly static string FlashcardDoesNotExist = "\nA flashcard with this name does not exist within the stack '{0}'!\n";

        public readonly static string SuccessfullyDeletedFlashcardMessage = "\nSuccessfully deleted flashcard from '{0}' stack!\n";

        // Stack 

        public readonly static string StackNamePrompt = "\nPlease enter a name for your new stack (up to 150 characters):\n";

        public readonly static string StackToDeleteNamePrompt = "\nPlease enter the exact name of the stack you wish to delete (case-insensitive):\nNOTE: ALL FLASHCARDS ASSOCIATED WITH THE STACK WILL ALSO BE DELETED.\n";

        public readonly static string StackDoesNotExistMessage = "\nStack with name '{0}' does not exist. Please try again!\n";

        public readonly static string SucccessfullyAddedStackMessage = "\nSuccessfully added new stack '{0}'!\n";

        public readonly static string SuccessfullyDeletedStackMessage = "\nSuccessfully deleted stack '{0}'!\n";

        public readonly static string InvalidStackNameMessage = "\nInvalid stack name. Please enter a non-null string of length equal to or less than 150 characters.\n";

    }


}
