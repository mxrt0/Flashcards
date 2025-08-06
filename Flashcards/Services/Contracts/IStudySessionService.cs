using Flashcards.Models;

namespace Flashcards.Services.Contracts
{
    public interface IStudySessionService
    {
        void AddStudySession(DateTime date, int score, int stackId);
        List<StudySession> GetAllStudySessions();
    }
}
