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
            Console.Clear();

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
                    Thread.Sleep(1000);
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
                    EditStack();
                    break;
                case 9:
                    ViewAllSessions();
                    break;
                case 10:
                    MonthlySessionsCountReport();
                    break;
                case 11:
                    MonthlyAverageScoreReport();
                    break;

            }
            MainMenu();
        }

        private void MonthlyAverageScoreReport()
        {
            Console.Clear();

            Console.WriteLine(Messages.MonthlyReportYearMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string yearInput = GetYearInput();

            Console.WriteLine($"\nAverage score report for year {yearInput}:\n");
            var report = _studySessionService.GetMonthlyAverageScoreByYear(int.Parse(yearInput));
            Console.WriteLine(string.Join($"\n{Environment.NewLine}", report));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void MonthlySessionsCountReport()
        {
            Console.Clear();

            Console.WriteLine(Messages.MonthlyReportYearMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string yearInput = GetYearInput();

            Console.WriteLine($"\nSession count report for year {yearInput}:\n");
            var report = _studySessionService.GetMonthlySessionsCountReportByYear(int.Parse(yearInput));
            Console.WriteLine(string.Join($"\n{Environment.NewLine}", report));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();

        }

        private void ViewAllSessions()
        {
            Console.WriteLine("\nYour study sessions: ");
            var sessions = _studySessionService.GetAllStudySessions();
            Console.WriteLine(string.Join(Environment.NewLine, sessions));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void EditStack()
        {

            Console.Clear();

            DisplayAllStacks();

            Console.WriteLine(Messages.StackToEditNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string stackToEditName = GetExistingStackInput();

            Console.WriteLine(Messages.StackNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string? newStackName = Console.ReadLine();
            CheckReturnToMainMenu(newStackName);

            while (!Validator.IsStackNameValid(newStackName))
            {
                Console.WriteLine(Messages.InvalidStackNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                newStackName = Console.ReadLine();
                CheckReturnToMainMenu(newStackName);
            }

            _stackService.EditStack(stackToEditName, newStackName);
            Console.WriteLine(string.Format(Messages.SuccessfullyEditedStackMessage, stackToEditName));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void EditFlashcard()
        {
            Console.Clear();

            DisplayAllStacks();

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

            string flashcardFront = GetFlashcardText();

            while (_flashcardService.GetFlashcard(flashcardFront, parentStack.Id) is not null)
            {
                Console.WriteLine(Messages.FlashcardAlreadyExistsMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardFront = Console.ReadLine();
                CheckReturnToMainMenu(flashcardFront);
            }

            Console.WriteLine(Messages.FlashcardBackPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string flashcardBack = GetFlashcardText();

            _flashcardService.EditFlashcard(flashcardToEdit.Front, flashcardFront, flashcardBack, parentStack.Id);
            Console.WriteLine(string.Format(Messages.SuccessfullyEditedFlashcardMessage, parentStack.Name));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void CreateNewStudySession()
        {
            Console.Clear();

            Console.WriteLine(Messages.NewStudySessionMessage);

            DisplayAllStacks();

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
                        break;
                    }
                    else if (string.Equals(userInput, "continue", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }
            }
            Console.WriteLine(string.Format(Messages.SessionConcludedMessage, currentScore, _stackService.GetNumberOfFlashcardsInStack(stackToStudy.Id)));
            _studySessionService.AddStudySession(DateTime.Now, currentScore, stackToStudy.Id);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
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

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void DeleteStack()
        {
            Console.Clear();

            DisplayAllStacks();

            Console.WriteLine(Messages.StackToDeleteNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string stackToDeleteName = GetExistingStackInput();

            _stackService.DeleteStack(stackToDeleteName);
            Console.WriteLine(string.Format(Messages.SuccessfullyDeletedStackMessage, stackToDeleteName));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void DeleteFlashcard()
        {
            Console.Clear();

            DisplayAllStacks();

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

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void AddStack()
        {
            Console.Clear();

            DisplayAllStacks();

            Console.WriteLine(Messages.StackNamePrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string stackName = GetStackNameInput();

            while (_stackService.GetStack(stackName) is not null)
            {
                Console.WriteLine(string.Format(Messages.StackAlreadyExistsMessage, stackName));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackName = GetStackNameInput();
            }
            _stackService.AddStack(stackName);
            Console.WriteLine(string.Format(Messages.SuccessfullyAddedStackMessage, stackName));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }


        private void AddFlashcard()
        {
            Console.Clear();

            DisplayAllStacks();

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

            string flashcardFront = GetFlashcardText();

            while (_flashcardService.GetFlashcard(flashcardFront, parentStack.Id) is not null)
            {
                Console.WriteLine(Messages.FlashcardAlreadyExistsMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardFront = Console.ReadLine();
                CheckReturnToMainMenu(flashcardFront);
            }

            Console.WriteLine(Messages.FlashcardBackPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            string flashcardBack = GetFlashcardText();

            _flashcardService.AddFlashcard(flashcardFront, flashcardBack, parentStack.Id);
            Console.WriteLine(string.Format(Messages.SuccessfullyAddedFlashcardMessage, parentStack.Name));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
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

        public void DisplayAllStacks()
        {
            Console.WriteLine("Your stacks:\n");
            var allStacks = _stackService.GetAllStacks();
            Console.WriteLine(string.Join($"\n- - - - - - - - - - - -{Environment.NewLine}", allStacks.Select(s => s.Name)));
        }

        public string GetYearInput()
        {
            string? yearInput = Console.ReadLine();
            CheckReturnToMainMenu(yearInput);

            while (!Validator.IsYearValid(yearInput))
            {
                Console.WriteLine(Messages.InvalidYearMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                yearInput = Console.ReadLine();
                CheckReturnToMainMenu(yearInput);

            }
            return yearInput;
        }
        public string GetExistingStackInput()
        {
            string? stackName = Console.ReadLine();
            CheckReturnToMainMenu(stackName);

            while (_stackService.GetStack(stackName) is null)
            {
                Console.WriteLine(string.Format(Messages.StackDoesNotExistMessage, stackName));
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackName = Console.ReadLine();
                CheckReturnToMainMenu(stackName);
            }
            return stackName;
        }
        public string GetStackNameInput()
        {
            string? stackName = Console.ReadLine();
            CheckReturnToMainMenu(stackName);

            while (!Validator.IsStackNameValid(stackName))
            {
                Console.WriteLine(Messages.InvalidStackNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                stackName = Console.ReadLine();
                CheckReturnToMainMenu(stackName);
            }
            return stackName;
        }
        public string GetFlashcardText()
        {
            string? flashcardText = Console.ReadLine();
            CheckReturnToMainMenu(flashcardText);

            while (!Validator.IsFlashcardTextValid(flashcardText))
            {
                Console.WriteLine(Messages.InvalidFlashcardTextMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                flashcardText = Console.ReadLine();
                CheckReturnToMainMenu(flashcardText);
            }
            return flashcardText;
        }
    }
}
