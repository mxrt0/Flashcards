namespace Flashcards.Utils
{
    public static class Messages
    {
        // Main

        public readonly static string ReturnToMainMenuMessage = "You can type 0 to return to the Main Menu:\n";

        public readonly static string PressAnyKeyToContinueMessage = "\nPress any key to continue...\n";

        // User Input

        public readonly static string InvalidInputMessage = $"\nInvalid input. Please enter a number between 0 and {UIHelper.NumberOfOptions}\n";


        // Flashcard

        public readonly static string FlashcardStackParentPrompt = "\nPlease enter the name of the stack your flash card will belong to (it must already exist):\n";

        public readonly static string FlashcardToDeleteStackParentPrompt = "\nPlease enter the name of the stack the flashcard you wish to delete belongs to (case-insensitive):\n";

        public readonly static string FlashcardToEditStackParentPrompt = "\nPlease enter the name of the stack the flashcard you wish to edit belongs to (case-insensitive):\n";

        public readonly static string FlashcardQuestionPrompt = "\nPlease enter the question on your flash card's front side (up to 255 characters):\n ";

        public readonly static string FlashcardToDeleteIDPrompt = "\nPlease enter the ID of the flashcard you wish to delete:\n ";

        public readonly static string FlashcardToEditIDPrompt = "\nPlease enter the ID of the flashcard you wish to edit:\n ";

        public readonly static string InvalidFlashcardIDMessage = "\nInvalid flashcard ID. Please enter a positive number less than or equal to the ID of the last flashcard in the stack:\n";

        public readonly static string FlashcardBackPrompt = "\nPlease enter the answer on your flash card's back side (up to 255 characters):\n ";

        public readonly static string InvalidFlashcardTextMessage = "\nInvalid flashcard text. Please enter a non-null string of length equal to or less than 255 characters.\n";

        public readonly static string SuccessfullyAddedFlashcardMessage = "\nSuccessfully added flashcard to stack '{0}'!\n";

        public readonly static string FlashcardAlreadyExistsMessage = "\nA flashcard with this question already exists in the stack you have selected. Please try again!\n";

        public readonly static string FlashcardDoesNotExistMessage = "\nA flashcard with this name does not exist within the stack '{0}'! Please try again: \n";

        public readonly static string SuccessfullyDeletedFlashcardMessage = "\nSuccessfully deleted flashcard from '{0}' stack!\n";

        public readonly static string SuccessfullyEditedFlashcardMessage = "\nSuccessfully edited flashcard from '{0}' stack!\n";

        // Stack 

        public readonly static string StackNamePrompt = "\nPlease enter a name for your stack (up to 150 characters):\n";

        public readonly static string StackToDeleteNamePrompt = "\nPlease enter the exact name of the stack you wish to delete (case-insensitive):\nNOTE: ALL FLASHCARDS ASSOCIATED WITH THE STACK WILL ALSO BE DELETED.\n";

        public readonly static string StackToEditNamePrompt = "\nPlease enter the exact name of the stack you wish to edit (case-insensitive):\n";

        public readonly static string StackToViewNamePrompt = "\nPlease enter the exact name of the stack you wish to view (case-insensitive):\n";

        public readonly static string StackToViewFlashcardCountPrompt = "\nPlease enter the number of flashcards you wish to retrieve from this stack. Enter 'all' to retrieve all: \n";

        public readonly static string StackDoesNotExistMessage = "\nStack with name '{0}' does not exist. Please try again!\n";

        public readonly static string StackAlreadyExistsMessage = "\nStack with name '{0}' already exists. Please try again!\n";

        public readonly static string InvalidFlashcardCountMessage = "\nInvalid flashcard count. Please enter 'All' or a non-negative number less than the total number of flashcards in the stack: \n";

        public readonly static string SuccessfullyAddedStackMessage = "\nSuccessfully added new stack '{0}'!\n";

        public readonly static string SuccessfullyEditedStackMessage = "\nSuccessfully edited stack '{0}'!\n";

        public readonly static string SuccessfullyDeletedStackMessage = "\nSuccessfully deleted stack '{0}'!\n";

        public readonly static string InvalidStackNameMessage = "\nInvalid stack name. Please enter a non-null string of length equal to or less than 150 characters.\n";

        // Study

        public readonly static string NewStudySessionMessage = "\nWelcome to the 'Study area'!\n";

        public readonly static string StudyStackMessage = "\nPlease choose a stack of flashcards to study: \n";

        public readonly static string AnswerQuestionPrompt = "\nYour answer to the question: ";

        public readonly static string CorrectAnswerMessage = "\nSpot on! Good job!\n";

        public readonly static string IncorrectAnswerMessage = "\nNot quite. The correct answer is actually '{0}'!\n";

        public readonly static string ConcludeSessionMessage = "\nType 'End' to conclude this study session and return to the Main Menu or type 'Continue' to continue:\n";

        public readonly static string InvalidSessionInputMessage = "\nPlease enter either 'End' or 'Continue':\n";

        public readonly static string SessionConcludedMessage = "\nYour study session has concluded. You got {0} right out of {1}\n";

    }


}
