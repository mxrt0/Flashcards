using Dapper;
using Flashcards.Models;
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

        public List<StudySession> GetAllStudySessions()
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var getAllQuery = @"SELECT 
                              ROW_NUMBER() OVER (ORDER BY Id) AS SessionId,
                              SessionDate,
                              Score FROM StudySession";
            return connection.Query<StudySession>(getAllQuery).ToList();
        }
    }
}
