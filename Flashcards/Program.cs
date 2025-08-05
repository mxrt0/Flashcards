using Flashcards.Controllers;
using Flashcards.Services;
using Flashcards.Services.Contracts;

namespace Flashcards
{
    public class Program
    {
        public static string connectionString = "Data Source=localhost;Initial Catalog=PracticeDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        static void Main(string[] args)
        {
            IStackService stackService = new StackService();
            IFlashcardService flashcardService = new FlashcardService();
            var controller = new FlashcardController(flashcardService, stackService);
        }
    }
}
