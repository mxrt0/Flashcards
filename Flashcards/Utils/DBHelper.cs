namespace Flashcards.Utils
{
    using System.Configuration;
    public static class DBHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

    }
}
