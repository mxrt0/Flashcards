namespace Flashcards.Services
{
    using Dapper;
    using Flashcards.DTOs;
    using Flashcards.Models;
    using Flashcards.Services.Contracts;
    using Flashcards.Utils;
    using Microsoft.Data.SqlClient;

    public class StackService : IStackService
    {
        public StackService()
        {

        }
        public void AddStack(string name)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string addCommand = @"INSERT INTO Stack(Name) VALUES(@Name)";
            connection.Execute(addCommand, new { Name = name });
        }

        public void DeleteStack(string name)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string delCommand = @"DELETE FROM Stack WHERE Name = @Name";
            connection.Execute(delCommand, new { Name = name });
        }

        public Stack? GetStack(string? name)
        {
            if (name is null)
            {
                return null;
            }
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string getQuery = @"SELECT TOP 1 * FROM Stack WHERE Name = @Name";
            return connection.QuerySingleOrDefault<Stack>(getQuery, new { Name = name.ToLower() });
        }

        public List<FlashcardDto> GetFlashcards(int stackId, int numberOfFlashcards)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string displayQuery = @$"SELECT TOP {numberOfFlashcards}
                                  ROW_NUMBER() OVER (ORDER BY Id) AS DisplayId, Front, Back FROM Flashcard 
                                  WHERE StackId = @Id ORDER BY Id
                                  ";
            return connection.Query<FlashcardDto>(displayQuery, new { Id = stackId }).ToList();
        }

        public int GetNumberOfFlashcardsInStack(int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string command = @"SELECT * FROM Flashcard WHERE StackId = @StackId";
            return connection.Query<Flashcard>(command, new { StackId = stackId }).Count();
        }
    }
}
