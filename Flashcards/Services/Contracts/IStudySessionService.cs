namespace Flashcards.Services.Contracts
{
    public interface IStudySessionService
    {
        void AddStudySession(DateTime date, int score, int stackId);

        void GetStudySession(int id);
    }
}
