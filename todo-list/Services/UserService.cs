// File: Services/UserService.cs
using System;
using System.Data.SQLite;
using todo_list.Database;
using todo_list.Models;

namespace todo_list.Services
{
    public class UserService
    {
        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @username AND Password = @password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool RegisterUser(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return false;

            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SQLiteException ex)
                    {
                        // Логирование ошибки или обработка исключения
                        return false;
                    }
                }
            }
        }
    }
}
