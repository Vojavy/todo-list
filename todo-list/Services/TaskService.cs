// File: Services/TaskService.cs
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using todo_list.Database;
using todo_list.Models;

namespace todo_list.Services
{
    public class TaskService
    {
        public List<Task> GetTasksForUser(int userId)
        {
            List<Task> tasks = new List<Task>();
            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = "SELECT * FROM Tasks WHERE UserId = @userId AND Status = 'undone'";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                TaskId = Convert.ToInt32(reader["TaskId"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Priority = reader["Priority"].ToString(),
                                Status = reader["Status"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                UserId = Convert.ToInt32(reader["UserId"])
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public void AddTask(Task task)
        {
            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = @"INSERT INTO Tasks (Title, Description, Priority, Status, CreatedDate, CategoryId, UserId)
                                 VALUES (@title, @description, @priority, @status, @createdDate, @categoryId, @userId)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@description", task.Description);
                    cmd.Parameters.AddWithValue("@priority", task.Priority);
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.Parameters.AddWithValue("@createdDate", task.CreatedDate);
                    cmd.Parameters.AddWithValue("@categoryId", task.CategoryId);
                    cmd.Parameters.AddWithValue("@userId", task.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = "DELETE FROM Tasks WHERE TaskId = @taskId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTask(Task task)
        {
            using (SQLiteConnection conn = DatabaseContext.GetConnection())
            {
                string query = @"UPDATE Tasks 
                                 SET Title = @title, Description = @description, Priority = @priority, 
                                     Status = @status, CategoryId = @categoryId 
                                 WHERE TaskId = @taskId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@description", task.Description);
                    cmd.Parameters.AddWithValue("@priority", task.Priority);
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.Parameters.AddWithValue("@categoryId", task.CategoryId);
                    cmd.Parameters.AddWithValue("@taskId", task.TaskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
