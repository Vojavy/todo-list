// File: Database/DatabaseInitializer.cs
using System.Data.SQLite;

namespace todo_list.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(SQLiteConnection connection)
        {
            string createUserTable = @"CREATE TABLE IF NOT EXISTS Users (
                                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                        Password TEXT NOT NULL
                                    );";
            string createCategoryTable = @"CREATE TABLE IF NOT EXISTS Categories (
                                            CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            CategoryName TEXT NOT NULL,
                                            UserId INTEGER,
                                            FOREIGN KEY(UserId) REFERENCES Users(UserId)
                                        );";
            string createTaskTable = @"CREATE TABLE IF NOT EXISTS Tasks (
                                        TaskId INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Title TEXT NOT NULL,
                                        Description TEXT,
                                        Priority TEXT,
                                        Status TEXT,
                                        CreatedDate DATETIME,
                                        CategoryId INTEGER,
                                        UserId INTEGER,
                                        FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId),
                                        FOREIGN KEY(UserId) REFERENCES Users(UserId)
                                    );";

            using (SQLiteCommand command = new SQLiteCommand(createUserTable, connection))
            {
                command.ExecuteNonQuery();
            }
            using (SQLiteCommand command = new SQLiteCommand(createCategoryTable, connection))
            {
                command.ExecuteNonQuery();
            }
            using (SQLiteCommand command = new SQLiteCommand(createTaskTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
