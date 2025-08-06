namespace Flashcards.Services
{
    using Dapper;
    using Flashcards.Models;
    using Flashcards.Services.Contracts;
    using Flashcards.Utils;
    using Microsoft.Data.SqlClient;
    using System.Collections.Generic;

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
            var delCommand = "DELETE FROM Flashcard WHERE StackId = @StackId AND Front = @Front";
            connection.Execute(delCommand, new { StackId = stackId, Front = front });
        }

        public void EditFlashcard(string currentFront, string newFront, string newBack, int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string editCommand = @"UPDATE Flashcard SET Front = @NewFront, Back = @NewBack 
                                    WHERE Front = @CurrentFront AND StackId = @StackId";
            connection.Execute(editCommand, new { NewFront = newFront, NewBack = newBack, CurrentFront = currentFront, StackId = stackId });
        }

        public Flashcard? GetFlashcard(string front, int stackId)
        {
            if (front is null)
            {
                return null;
            }
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();

            string getQuery = @"SELECT TOP 1 * FROM Flashcard WHERE StackId = @StackId AND Front = @Front";

            return connection.QuerySingleOrDefault<Flashcard>(getQuery, new { StackId = stackId, Front = front });
        }

        public List<Flashcard> GetFlashcardsInStack(int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();

            string getQuery = @"SELECT * FROM Flashcard WHERE StackId = @StackId";

            return connection.Query<Flashcard>(getQuery, new { StackId = stackId }).ToList();
        }
    }
}
