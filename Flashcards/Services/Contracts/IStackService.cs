namespace Flashcards.Services.Contracts
{
    using Flashcards.DTOs;
    using Flashcards.Models;
    public interface IStackService
    {
        void AddStack(string name);

        void DeleteStack(string name);

        Stack GetStack(string name);

        List<FlashcardDto> GetFlashcards(int id, int flashcardsNum);

        int GetNumberOfFlashcardsInStack(int id);

        List<Stack> GetAllStacks();

        void EditStack(string currentName, string newName);
    }
}
