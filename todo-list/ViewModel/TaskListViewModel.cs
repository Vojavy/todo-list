using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using todo_list.Models;
using todo_list.Services;

namespace todo_list.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Task> _tasks;
        private TaskService _taskService;

        public TaskListViewModel(List<int> themeIds)
        {
            _taskService = new TaskService();
            Tasks = new ObservableCollection<Task>(_taskService.GetTasksForThemes(themeIds));
        }

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set { _tasks = value; OnPropertyChanged(); }
        }

        // Дополнительные свойства и команды для управления задачами

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
