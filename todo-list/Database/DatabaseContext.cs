// File: Database/DatabaseContext.cs
using System.Data.SQLite;

namespace todo_list.Database
{
    public static class DatabaseContext
    {
        private static string _connectionString = "Data Source=todo.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
