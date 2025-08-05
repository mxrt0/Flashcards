namespace Flashcards.Services.Contracts
{
    using Flashcards.Models;
    public interface IStackService
    {
        void AddStack(string name);

        void DeleteStack(string name);

        Stack GetStack(string name);
    }
}
