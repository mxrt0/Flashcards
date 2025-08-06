using Dapper;
using Flashcards.Services.Contracts;
using Flashcards.Utils;
using Microsoft.Data.SqlClient;

namespace Flashcards.Services
{
    public class StudySessionService : IStudySessionService
    {
        public void AddStudySession(DateTime date, int score, int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var addCommand = @"INSERT INTO StudySession(SessionDate, Score, StackId) VALUES(@Date, @Score, @StackId)";
            connection.Execute(addCommand, new { Date = date, Score = score, StackId = stackId });
        }
        public void GetStudySession(int id)
        {
            throw new NotImplementedException();
        }
    }
}
