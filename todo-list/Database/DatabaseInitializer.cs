using System.Data.SQLite;
using System.IO;

namespace todo_list.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                // Создание таблицы пользователей
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Users (
                                            UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Username TEXT NOT NULL UNIQUE,
                                            Password TEXT NOT NULL
                                        );";
                command.ExecuteNonQuery();

                // Создание таблицы тем с уникальным ограничением
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Themes (
                                            ThemeId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            ThemeName TEXT NOT NULL,
                                            UserId INTEGER NOT NULL,
                                            FOREIGN KEY(UserId) REFERENCES Users(UserId),
                                            UNIQUE (ThemeName, UserId)
                                        );";
                command.ExecuteNonQuery();

                // Создание таблицы задач
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Tasks (
                                            TaskId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Title TEXT NOT NULL,
                                            Description TEXT,
                                            Priority TEXT NOT NULL,
                                            Status TEXT NOT NULL,
                                            CreatedDate TEXT NOT NULL,
                                            ThemeId INTEGER NOT NULL,
                                            UserId INTEGER NOT NULL,
                                            FOREIGN KEY(ThemeId) REFERENCES Themes(ThemeId),
                                            FOREIGN KEY(UserId) REFERENCES Users(UserId)
                                        );";
                command.ExecuteNonQuery();
            }
        }
    }
}
