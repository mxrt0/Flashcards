namespace Flashcards.Controllers
{
    using Flashcards.Models;
    using Flashcards.Services.Contracts;
    using Flashcards.Utils;

    public class FlashcardController
    {
        private readonly IFlashcardService _flashcardService;
        private readonly IStackService _stackService;
        private readonly IStudySessionService _studySessionService;
        public FlashcardController(IFlashcardService flashcardService, IStackService stackService, IStudySessionService studySessionService)
        {
            _flashcardService = flashcardService;
            _stackService = stackService;
            _studySessionService = studySessionService;
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
                case 6:
                    CreateNewStudySession();
                    break;
                case 7:
                    EditFlashcard();
                    break;
                case 8:
                    //EditStack();
                    break;
            }
            MainMenu();
        }

        private void EditFlashcard()
        {
            Console.Clear();

            Console.WriteLine(Messages.FlashcardToEditStackParentPrompt);
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
            var allFlashcardsCount = _stackService.GetNumberOfFlashcardsInStack(parentStack.Id);
            var flashcardsInStack = _stackService.GetFlashcards(parentStack.Id, allFlashcardsCount);
            Console.WriteLine(string.Join(Environment.NewLine, flashcardsInStack));

            Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine(Messages.FlashcardToEditIDPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
            string? flashcardToEditID = Console.ReadLine();
            CheckReturnToMainMenu(flashcardToEditID);

            while (!int.TryParse(flashcardToEditID, out _) || int.Parse(flashcardToEditID) < 0
                || int.Parse(flashcardToEditID) > allFlashcardsCount)
            {
                Console.WriteLine(Messages.InvalidFlashcardIDMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardToEditID = Console.ReadLine();
                CheckReturnToMainMenu(flashcardToEditID);
            }

            var flashcardToEdit = flashcardsInStack[int.Parse(flashcardToEditID) - 1];

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


            _flashcardService.EditFlashcard(flashcardToEdit.Front, flashcardFront, flashcardBack, parentStack.Id);
            Console.WriteLine(string.Format(Messages.SuccessfullyEditedFlashcardMessage, parentStack.Name));
        }

        private void CreateNewStudySession()
        {
            Console.WriteLine(Messages.NewStudySessionMessage);

            Console.WriteLine(Messages.StudyStackMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? stackToStudyName = Console.ReadLine();
            CheckReturnToMainMenu(stackToStudyName);
            Stack? stackToStudy = _stackService.GetStack(stackToStudyName);

            while (stackToStudy is null)
            {
                Console.WriteLine(Messages.StackDoesNotExistMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackToStudyName = Console.ReadLine();
                if (CheckReturnToMainMenu(stackToStudyName))
                {
                    break;
                }
                stackToStudy = _stackService.GetStack(stackToStudyName);
            }

            int currentScore = 0;

            var flashcards = _flashcardService.GetFlashcardsInStack(stackToStudy.Id);

            foreach (var flashcard in flashcards)
            {
                Console.WriteLine($"\n{flashcard.Front}");

                Console.Write(Messages.AnswerQuestionPrompt);

                string? userAnswer = Console.ReadLine();

                if (flashcard.Back == userAnswer)
                {
                    Console.WriteLine(Messages.CorrectAnswerMessage);
                    currentScore++;
                }
                else
                {
                    Console.WriteLine(string.Format(Messages.IncorrectAnswerMessage, flashcard.Back));
                }

                Console.WriteLine(Messages.ConcludeSessionMessage);
                string? userInput = Console.ReadLine();
                if (string.Equals(userInput, "end", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(string.Format(Messages.SessionConcludedMessage, currentScore, _stackService.GetNumberOfFlashcardsInStack(stackToStudy.Id)));
                    break;
                }
                else if (string.Equals(userInput, "continue", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                else
                {
                    while (!string.Equals(userInput, "end", StringComparison.OrdinalIgnoreCase)
                        && !string.Equals(userInput, "continue", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(Messages.InvalidSessionInputMessage);
                        userInput = Console.ReadLine();
                    }

                    if (string.Equals(userInput, "end", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(string.Format(Messages.SessionConcludedMessage, currentScore, _stackService.GetNumberOfFlashcardsInStack(stackToStudy.Id)));
                        break;
                    }
                    else if (string.Equals(userInput, "continue", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }
            }
            _studySessionService.AddStudySession(DateTime.Now, currentScore, stackToStudy.Id);
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
            int numberOfAllFlashcards = _stackService.GetNumberOfFlashcardsInStack(stackToDisplay.Id);

            Console.WriteLine(Messages.StackToViewFlashcardCountPrompt);
            string? flashcardCount = Console.ReadLine();
            int numberOfFlashcardsToDisplay = 0;
            if (string.Equals(flashcardCount, "all", StringComparison.OrdinalIgnoreCase))
            {
                numberOfFlashcardsToDisplay = numberOfAllFlashcards;
            }
            else
            {
                while (!int.TryParse(flashcardCount, out _)
                 || int.Parse(flashcardCount) < 0 || int.Parse(flashcardCount) > numberOfAllFlashcards)
                {
                    Console.WriteLine(Messages.InvalidFlashcardCountMessage);
                    Console.WriteLine(Messages.ReturnToMainMenuMessage);
                    flashcardCount = Console.ReadLine();
                    CheckReturnToMainMenu(flashcardCount);
                    if (string.Equals(flashcardCount, "all", StringComparison.OrdinalIgnoreCase))
                    {
                        numberOfFlashcardsToDisplay = numberOfAllFlashcards;
                        break;
                    }
                }

                if (numberOfFlashcardsToDisplay != numberOfAllFlashcards)
                {
                    numberOfFlashcardsToDisplay = int.Parse(flashcardCount);
                }
            }

            Console.WriteLine($"\nStack '{stackToDisplay.Name}':\n");
            Console.WriteLine(string.Join(Environment.NewLine, _stackService.GetFlashcards(stackToDisplay.Id, numberOfFlashcardsToDisplay)));
        }

        private void DeleteStack()
        {
            Console.Clear();

            Console.WriteLine("Your stacks:\n");
            var allStacks = _stackService.GetAllStacks();
            Console.WriteLine(string.Join($"\n- - - - - - - - - - - -{Environment.NewLine}", allStacks.Select(s => s.Name)));

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
            Console.Clear();

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
            var allFlashcardsCount = _stackService.GetNumberOfFlashcardsInStack(parentStack.Id);
            var flashcardsInStack = _stackService.GetFlashcards(parentStack.Id, allFlashcardsCount);
            Console.WriteLine(string.Join(Environment.NewLine, flashcardsInStack));

            Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine(Messages.FlashcardToDeleteIDPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
            string? flashcardToDeleteID = Console.ReadLine();
            CheckReturnToMainMenu(flashcardToDeleteID);

            while (!int.TryParse(flashcardToDeleteID, out _) || int.Parse(flashcardToDeleteID) < 0
                || int.Parse(flashcardToDeleteID) > allFlashcardsCount)
            {
                Console.WriteLine(Messages.InvalidFlashcardIDMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardToDeleteID = Console.ReadLine();
                CheckReturnToMainMenu(flashcardToDeleteID);
            }

            var flashcardToDelete = flashcardsInStack[int.Parse(flashcardToDeleteID) - 1];
            _flashcardService.DeleteFlashcard(flashcardToDelete.Front, parentStack.Id);
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
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
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

        public bool CheckReturnToMainMenu(string? input = "")
        {
            if (input == "0")
            {
                MainMenu();
                return true;
            }
            return false;
        }
    }
}
