namespace Flashcards.Controllers
{
    using Flashcards.Models;
    using Flashcards.Services.Contracts;
    using Flashcards.Utils;

    public class FlashcardController
    {
        private readonly IFlashcardService _flashcardService;
        private readonly IStackService _stackService;
        public FlashcardController(IFlashcardService flashcardService, IStackService stackService)
        {
            _flashcardService = flashcardService;
            _stackService = stackService;
            MainMenu();
        }
        public void MainMenu()
        {
            UIHelper.DisplayOptions();
            string? userInput = Console.ReadLine();
            while (!Validator.IsUserInputValid(userInput))
            {
                Console.WriteLine(Messages.InvalidInputMessage);
                userInput = Console.ReadLine();
            }
            HandleUserInput(int.Parse(userInput));
        }
        public void HandleUserInput(int input)
        {
            switch (input)
            {
                case 0:
                    Console.WriteLine("\nGoodbye!\n");
                    Environment.Exit(0);
                    break;
                case 1:
                    AddFlashcard();
                    break;
                case 2:
                    DeleteFlashcard();
                    break;
                case 3:
                    AddStack();
                    break;
                case 4:
                    DisplayStack();
                    break;
                case 5:
                    DeleteStack();
                    break;
            }
            MainMenu();
        }

        private void DisplayStack()
        {
            Console.Clear();

            Console.WriteLine(Messages.StackToViewNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? stackToDisplayName = Console.ReadLine();
            CheckReturnToMainMenu(stackToDisplayName);
            Stack? stackToDisplay = _stackService.GetStack(stackToDisplayName);
            while (stackToDisplay is null)
            {
                Console.WriteLine(string.Format(Messages.StackDoesNotExistMessage, stackToDisplayName));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackToDisplayName = Console.ReadLine();
                CheckReturnToMainMenu(stackToDisplayName);
                stackToDisplay = _stackService.GetStack(stackToDisplayName);
            }
            Console.WriteLine($"\nStack '{stackToDisplay.Name}':\n");
            Console.WriteLine(string.Join(Environment.NewLine, _stackService.DisplayStack(stackToDisplay.Id)));
        }

        private void DeleteStack()
        {
            Console.Clear();

            Console.WriteLine(Messages.StackToDeleteNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? stackToDeleteName = Console.ReadLine();
            CheckReturnToMainMenu(stackToDeleteName);

            while (_stackService.GetStack(stackToDeleteName) is null)
            {
                Console.WriteLine(string.Format(Messages.StackDoesNotExistMessage, stackToDeleteName));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackToDeleteName = Console.ReadLine();
                CheckReturnToMainMenu(stackToDeleteName);
            }

            _stackService.DeleteStack(stackToDeleteName);
            Console.WriteLine(string.Format(Messages.SuccessfullyDeletedStackMessage, stackToDeleteName));
        }

        private void DeleteFlashcard()
        {
            Console.WriteLine(Messages.FlashcardToDeleteStackParentPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? parentName = Console.ReadLine();
            CheckReturnToMainMenu(parentName);

            Stack? parentStack = _stackService.GetStack(parentName.ToLower());
            while (parentStack is null)
            {
                Console.WriteLine(string.Format(Messages.InvalidStackNameMessage, parentName));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                parentName = Console.ReadLine();
                CheckReturnToMainMenu(parentName);
            }

            Console.WriteLine(Messages.FlashcardToDeleteQuestionPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? flashcardToDeleteFront = Console.ReadLine();
            CheckReturnToMainMenu(flashcardToDeleteFront);

            while (_flashcardService.GetFlashcard(flashcardToDeleteFront, parentStack.Id) is null)
            {
                Console.WriteLine(string.Format(Messages.FlashcardDoesNotExistMessage, parentStack.Name));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardToDeleteFront = Console.ReadLine();
                CheckReturnToMainMenu(flashcardToDeleteFront);
            }

            _flashcardService.DeleteFlashcard(flashcardToDeleteFront, parentStack.Id);
            Console.WriteLine(string.Format(Messages.SuccessfullyDeletedFlashcardMessage, parentStack.Name));
        }

        private void AddStack()
        {
            Console.Clear();

            Console.WriteLine(Messages.StackNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? stackName = Console.ReadLine();
            CheckReturnToMainMenu(stackName);

            while (!Validator.IsStackNameValid(stackName))
            {
                Console.WriteLine(Messages.InvalidStackNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackName = Console.ReadLine();
                CheckReturnToMainMenu(stackName);
            }

            _stackService.AddStack(stackName);
            Console.WriteLine(string.Format(Messages.SucccessfullyAddedStackMessage, stackName));
        }


        private void AddFlashcard()
        {
            Console.Clear();

            Console.WriteLine(Messages.FlashcardStackParentPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? parentName = Console.ReadLine();
            CheckReturnToMainMenu(parentName);

            Stack? parentStack = _stackService.GetStack(parentName);
            while (parentStack is null)
            {
                Console.WriteLine(string.Format(Messages.StackDoesNotExistMessage, parentName));
                Console.Write(Messages.ReturnToMainMenuMessage);
                parentName = Console.ReadLine();
                CheckReturnToMainMenu(parentName);
                parentStack = _stackService.GetStack(parentName);
            }

            Console.WriteLine(Messages.FlashcardQuestionPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? flashcardFront = Console.ReadLine();
            CheckReturnToMainMenu(parentName);

            while (!Validator.IsFlashcardTextValid(flashcardFront))
            {
                Console.WriteLine(Messages.InvalidFlashcardTextMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardFront = Console.ReadLine();
                CheckReturnToMainMenu(flashcardFront);
            }

            while (_flashcardService.GetFlashcard(flashcardFront, parentStack.Id) is not null)
            {
                Console.WriteLine(Messages.FlashcardAlreadyExistsMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardFront = Console.ReadLine();
                CheckReturnToMainMenu(flashcardFront);
            }

            Console.WriteLine(Messages.FlashcardBackPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? flashcardBack = Console.ReadLine();
            CheckReturnToMainMenu(flashcardBack);

            while (!Validator.IsFlashcardTextValid(flashcardBack))
            {
                Console.WriteLine(Messages.InvalidFlashcardTextMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardBack = Console.ReadLine();
                CheckReturnToMainMenu(flashcardBack);
            }

            _flashcardService.AddFlashcard(flashcardFront, flashcardBack, parentStack.Id);
            Console.WriteLine(string.Format(Messages.SuccessfullyAddedFlashcardMessage, parentStack.Name));
        }

        public void CheckReturnToMainMenu(string? input = "")
        {
            if (input == "0")
            {
                MainMenu();
            }
        }
    }
}
