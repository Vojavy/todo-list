// File: Services/CategoryService.cs
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using todo_list.Database;
using todo_list.Models;

namespace todo_list.Services
{
    public class CategoryService
    {
        /// <summary>
        /// Получает список категорий для указанного пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <returns>Список категорий.</returns>
        public List<Category> GetCategoriesForUser(int userId)
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SQLiteConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Categories WHERE UserId = @userId";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    UserId = Convert.ToInt32(reader["UserId"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки или уведомление пользователя
                MessageBox.Show($"Error retrieving categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return categories;
        }

        /// <summary>
        /// Добавляет новую категорию в базу данных.
        /// </summary>
        /// <param name="category">Категория для добавления.</param>
        public void AddCategory(Category category)
        {
            try
            {
                using (SQLiteConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "INSERT INTO Categories (CategoryName, UserId) VALUES (@name, @userId)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", category.CategoryName);
                        cmd.Parameters.AddWithValue("@userId", category.UserId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки или уведомление пользователя
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw; // Повторное выбрасывание исключения для обработки в ViewModel
            }
        }

        /// <summary>
        /// Удаляет категорию из базы данных по ее ID.
        /// </summary>
        /// <param name="categoryId">ID категории для удаления.</param>
        public void DeleteCategory(int categoryId)
        {
            try
            {
                using (SQLiteConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "DELETE FROM Categories WHERE CategoryId = @categoryId";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@categoryId", categoryId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки или уведомление пользователя
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw; // Повторное выбрасывание исключения для обработки в ViewModel
            }
        }

        /// <summary>
        /// Получает ID последней вставленной категории.
        /// </summary>
        /// <returns>ID последней вставленной категории или -1 в случае ошибки.</returns>
        public int GetLastInsertedCategoryId()
        {
            try
            {
                using (SQLiteConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT last_insert_rowid()";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int lastId))
                        {
                            return lastId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving last inserted Category ID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1; // Значение по умолчанию при ошибке
        }
    }
}
