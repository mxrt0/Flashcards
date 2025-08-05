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
            string search = front;
            var terms = search.Split(' ');
            var delCommand = "DELETE FROM Flashcard WHERE StackId = @StackId";

            for (int i = 0; i < terms.Length; i++)
            {
                delCommand += $" AND Front LIKE @term{i}";
            }

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("StackId", stackId);

            for (int i = 0; i < terms.Length; i++)
            {
                dynamicParams.Add($"term{i}", $"%{terms[i]}%");
            }
            connection.Execute(delCommand, dynamicParams);
        }

        public Flashcard? GetFlashcard(string front, int stackId)
        {
            if (front is null)
            {
                return null;
            }
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            string search = front;
            var terms = search.Split(' ');
            string getQuery = @"SELECT TOP 1 * FROM Flashcard WHERE StackId = @StackId";

            for (int i = 0; i < terms.Length; i++)
            {
                getQuery += $" AND Front LIKE @term{i}";
            }

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("StackId", stackId);

            for (int i = 0; i < terms.Length; i++)
            {
                dynamicParams.Add($"term{i}", $"%{terms[i]}%");
            }

            return connection.QuerySingleOrDefault<Flashcard>(getQuery, dynamicParams);
        }
    }
}
