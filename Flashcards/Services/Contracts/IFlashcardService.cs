using Flashcards.Models;

namespace Flashcards.Services.Contracts
{
    public interface IFlashcardService
    {
        void AddFlashcard(string front, string back, int stackId);

        void DeleteFlashcard(string front, int stackId);

        Flashcard? GetFlashcard(string front, int stackId);
    }
}
