using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using todo_list.Database;
using todo_list.Models;

namespace todo_list.Services
{
    public class ThemeService
    {
        // Получение списка тем для конкретного пользователя
        public List<Theme> GetThemesForUser(int userId)
        {
            List<Theme> themes = new List<Theme>();

            string query = "SELECT ThemeId, ThemeName FROM Themes WHERE UserId = @UserId";

            using (var command = new SQLiteCommand(query, DatabaseContext.Instance.Connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        themes.Add(new Theme
                        {
                            ThemeId = reader.GetInt32(0),
                            ThemeName = reader.GetString(1),
                            IsSelected = false
                        });
                    }
                }
            }

            return themes;
        }

        // Добавление новой темы
        public bool AddTheme(Theme theme, int userId)
        {
            try
            {
                // Проверка, существует ли тема с таким именем для пользователя
                if (ThemeNameExists(theme.ThemeName, userId))
                {
                    return false; // Тема с таким именем уже существует
                }

                string insertQuery = "INSERT INTO Themes (ThemeName, UserId) VALUES (@ThemeName, @UserId)";

                using (var command = new SQLiteCommand(insertQuery, DatabaseContext.Instance.Connection))
                {
                    command.Parameters.AddWithValue("@ThemeName", theme.ThemeName);
                    command.Parameters.AddWithValue("@UserId", userId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Получение ID только что добавленной темы
                        command.CommandText = "SELECT last_insert_rowid()";
                        theme.ThemeId = Convert.ToInt32(command.ExecuteScalar());
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Логирование ошибки или уведомление пользователя
                System.Diagnostics.Debug.WriteLine($"Error adding theme: {ex.Message}");
                return false;
            }
        }

        // Удаление тем по их ID
        public void DeleteThemes(List<int> themeIds)
        {
            if (themeIds == null || themeIds.Count == 0)
                return;

            using (var transaction = DatabaseContext.Instance.Connection.BeginTransaction())
            {
                foreach (var themeId in themeIds)
                {
                    string deleteQuery = "DELETE FROM Themes WHERE ThemeId = @ThemeId";

                    using (var command = new SQLiteCommand(deleteQuery, DatabaseContext.Instance.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ThemeId", themeId);
                        command.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
        }

        // Проверка, существует ли тема с таким именем для пользователя
        private bool ThemeNameExists(string themeName, int userId)
        {
            string query = "SELECT COUNT(1) FROM Themes WHERE ThemeName = @ThemeName AND UserId = @UserId";

            using (var command = new SQLiteCommand(query, DatabaseContext.Instance.Connection))
            {
                command.Parameters.AddWithValue("@ThemeName", themeName);
                command.Parameters.AddWithValue("@UserId", userId);

                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
