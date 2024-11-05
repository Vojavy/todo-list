using System;
using System.Data.SQLite;
using todo_list.Database;

namespace todo_list.Services
{
    public class UserService
    {
        public int AuthenticateUser(string username, string password)
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                command.CommandText = "SELECT UserId FROM Users WHERE Username = @username AND Password = @password";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        public string RegisterUser(string username, string password)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
                {
                    command.CommandText = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                    return null; // Регистрация успешна
                }
            }
            catch (SQLiteException ex)
            {
                // Возвращаем сообщение об ошибке
                return ex.Message;
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                return ex.Message;
            }
        }

        public bool IsUsernameTaken(string username)
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                command.CommandText = "SELECT COUNT(1) FROM Users WHERE Username = @username";
                command.Parameters.AddWithValue("@username", username);

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
