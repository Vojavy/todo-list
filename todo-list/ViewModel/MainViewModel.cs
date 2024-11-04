// File: ViewModels/MainViewModel.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using todo_list.Commands;
using todo_list.Models;
using todo_list.Services;
using todo_list.Views;

namespace todo_list.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TaskService _taskService;
        private readonly CategoryService _categoryService;
        private readonly UserService _userService;
        private readonly IDialogService _dialogService;

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(nameof(Categories)); }
        }

        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; OnPropertyChanged(nameof(Tasks)); }
        }

        private IList<Category> _selectedCategories;
        public IList<Category> SelectedCategories
        {
            get { return _selectedCategories; }
            set
            {
                _selectedCategories = value; OnPropertyChanged(nameof(SelectedCategories));
                // Обновляем команду DeleteCategoryCommand при изменении выбранных категорий
                ((RelayCommand)DeleteCategoryCommand).RaiseCanExecuteChanged();
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand MarkTaskCompletedCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand IncreasePriorityCommand { get; }
        public ICommand DecreasePriorityCommand { get; }
        public ICommand ShowTasksCommand { get; }
        public ICommand OpenCalendarCommand { get; }

        public MainViewModel()
        {
            _taskService = new TaskService();
            _categoryService = new CategoryService();
            _userService = new UserService();
            _dialogService = new DialogService(); // Инициализация сервиса диалогов

            // Инициализация текущего пользователя (для примера)
            CurrentUser = new User { UserId = 1, Username = "test", Password = "password" };

            LoadData();

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanModifyTask);
            MarkTaskCompletedCommand = new RelayCommand(MarkTaskCompleted, CanModifyTask);
            AddCategoryCommand = new RelayCommand(AddCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory, CanDeleteCategory);
            IncreasePriorityCommand = new RelayCommand(IncreasePriority);
            DecreasePriorityCommand = new RelayCommand(DecreasePriority);
            ShowTasksCommand = new RelayCommand(ShowTasks);
            OpenCalendarCommand = new RelayCommand(OpenCalendar);
        }

        private void LoadData()
        {
            Categories = new ObservableCollection<Category>(_categoryService.GetCategoriesForUser(CurrentUser.UserId));
            Tasks = new ObservableCollection<Task>(_taskService.GetTasksForUser(CurrentUser.UserId));
        }

        private void AddTask(object parameter)
        {
            // Реализуйте логику добавления задачи
            // Например, открыть диалог для добавления новой задачи
            // В этом примере опустим реализацию
            MessageBox.Show("Add Task functionality is not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteTask(object parameter)
        {
            if (parameter is Task task)
            {
                _taskService.DeleteTask(task.TaskId);
                Tasks.Remove(task);
            }
        }

        private void MarkTaskCompleted(object parameter)
        {
            if (parameter is Task task)
            {
                task.Status = "done";
                _taskService.UpdateTask(task);
                Tasks.Remove(task);
            }
        }

        private bool CanModifyTask(object parameter)
        {
            return parameter is Task;
        }

        private void AddCategory(object parameter)
        {
            // Открываем диалог для ввода названия категории
            string newCategoryName = _dialogService.ShowInputDialog("Add Category", "Enter category name:");
            if (!string.IsNullOrWhiteSpace(newCategoryName))
            {
                try
                {
                    Category newCategory = new Category
                    {
                        CategoryName = newCategoryName,
                        UserId = CurrentUser.UserId
                    };

                    _categoryService.AddCategory(newCategory);

                    // Получаем последний вставленный CategoryId
                    newCategory.CategoryId = _categoryService.GetLastInsertedCategoryId();

                    Categories.Add(newCategory);
                    _dialogService.ShowMessage("Category added successfully.", "Success", MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    _dialogService.ShowMessage($"Error adding category: {ex.Message}", "Error", MessageBoxImage.Error);
                }
            }
        }

        private bool CanDeleteCategory(object parameter)
        {
            return SelectedCategories != null && SelectedCategories.Count > 0;
        }

        private void DeleteCategory(object parameter)
        {
            if (SelectedCategories != null && SelectedCategories.Count > 0)
            {
                var categoriesToDelete = new List<Category>(SelectedCategories);
                var result = MessageBox.Show("Are you sure you want to delete the selected categories?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var category in categoriesToDelete)
                    {
                        try
                        {
                            _categoryService.DeleteCategory(category.CategoryId);
                            Categories.Remove(category);
                        }
                        catch (Exception ex)
                        {
                            _dialogService.ShowMessage($"Error deleting category '{category.CategoryName}': {ex.Message}", "Error", MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void IncreasePriority(object parameter)
        {
            if (parameter is Task task)
            {
                switch (task.Priority)
                {
                    case "LOW":
                        task.Priority = "MID";
                        break;
                    case "MID":
                        task.Priority = "HIGH";
                        break;
                    case "HIGH":
                        break;
                }
                _taskService.UpdateTask(task);
                // Обновляем коллекцию, чтобы UI обновился
                int index = Tasks.IndexOf(task);
                if (index >= 0)
                {
                    Tasks[index] = task;
                }
            }
        }

        private void DecreasePriority(object parameter)
        {
            if (parameter is Task task)
            {
                switch (task.Priority)
                {
                    case "HIGH":
                        task.Priority = "MID";
                        break;
                    case "MID":
                        task.Priority = "LOW";
                        break;
                    case "LOW":
                        break;
                }
                _taskService.UpdateTask(task);
                // Обновляем коллекцию, чтобы UI обновился
                int index = Tasks.IndexOf(task);
                if (index >= 0)
                {
                    Tasks[index] = task;
                }
            }
        }

        private void ShowTasks(object parameter)
        {
            // Реализуйте логику отображения задач
            MessageBox.Show("Show Tasks functionality is not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenCalendar(object parameter)
        {
            // Реализуйте логику открытия календаря
            MessageBox.Show("Open Calendar functionality is not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
