namespace Flashcards.Services
{
    using Dapper;
    using Flashcards.Models;
    using Flashcards.Services.Contracts;
    using Flashcards.Utils;
    using Microsoft.Data.SqlClient;

    public class FlashcardService : IFlashcardService
    {
        public FlashcardService()
        {

        }

        public void AddFlashcard(string front, string back, int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string addCommand = @"INSERT INTO Flashcard(Front, Back, StackId) VALUES(@Front, @Back, @StackId)";
            connection.Execute(addCommand, new { Front = front, Back = back, StackId = stackId });
        }

        public void DeleteFlashcard(string front, int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string delCommand = @"DELETE FROM Flashcard WHERE Front = @Front AND StackId = @StackId";
            connection.Execute(delCommand, new { Front = front, StackId = stackId });
        }

        public Flashcard? GetFlashcard(string? front, int stackId)
        {
            if (front is null)
            {
                return null;
            }
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string getQuery = @"SELECT TOP 1 * FROM Flashcard WHERE Front = @Front AND StackId = @StackId";
            return connection.QuerySingleOrDefault<Flashcard>(getQuery, new { Front = front, StackId = stackId });
        }
    }
}
