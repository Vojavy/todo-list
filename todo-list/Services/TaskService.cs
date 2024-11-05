using System.Collections.Generic;
using System.Data.SQLite;
using todo_list.Database;
using todo_list.Models;

namespace todo_list.Services
{
    public class TaskService
    {
        public List<Task> GetTasksForThemes(List<int> themeIds)
        {
            var tasks = new List<Task>();
            string ids = string.Join(",", themeIds);
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                command.CommandText = $"SELECT TaskId, Title, Description, Priority, Status, CreatedDate, ThemeId, UserId FROM Tasks WHERE ThemeId IN ({ids})";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Priority = reader.GetString(3),
                            Status = reader.GetString(4),
                            CreatedDate = reader.GetString(5),
                            ThemeId = reader.GetInt32(6),
                            UserId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return tasks;
        }

        public void AddTask(Task task)
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                command.CommandText = @"INSERT INTO Tasks (Title, Description, Priority, Status, CreatedDate, ThemeId, UserId) 
                                        VALUES (@title, @description, @priority, @status, @createdDate, @themeId, @userId)";
                command.Parameters.AddWithValue("@title", task.Title);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@priority", task.Priority);
                command.Parameters.AddWithValue("@status", task.Status);
                command.Parameters.AddWithValue("@createdDate", task.CreatedDate);
                command.Parameters.AddWithValue("@themeId", task.ThemeId);
                command.Parameters.AddWithValue("@userId", task.UserId);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateTaskStatus(int taskId, string status)
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                command.CommandText = "UPDATE Tasks SET Status = @status WHERE TaskId = @taskId";
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@taskId", taskId);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteTasks(List<int> taskIds)
        {
            using (var command = new SQLiteCommand(DatabaseContext.Instance.Connection))
            {
                foreach (var taskId in taskIds)
                {
                    command.CommandText = "DELETE FROM Tasks WHERE TaskId = @taskId";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
