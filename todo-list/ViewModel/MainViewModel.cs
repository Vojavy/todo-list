using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using todo_list.Commands;
using todo_list.Models;
using todo_list.Services;
using todo_list.Views;


namespace todo_list.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Theme> _themes;
        private object _currentView;
        private readonly ThemeService _themeService;

        public MainViewModel()
        {
            _themeService = new ThemeService();
            Themes = new ObservableCollection<Theme>(_themeService.GetThemesForUser(UserSession.CurrentUserId));

            ShowTasksCommand = new RelayCommand(ShowTasks, CanShowContent);
            ShowCalendarCommand = new RelayCommand(ShowCalendar, CanShowContent);
            AddThemeCommand = new RelayCommand(AddTheme);
            DeleteSelectedThemesCommand = new RelayCommand(DeleteSelectedThemes, CanDeleteThemes);

            // Подписываемся на изменение свойств в коллекции Themes
            foreach (var theme in Themes)
            {
                theme.PropertyChanged += Theme_PropertyChanged;
            }
            Themes.CollectionChanged += Themes_CollectionChanged;
        }

        public ObservableCollection<Theme> Themes
        {
            get => _themes;
            set { _themes = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Theme> SelectedThemes
        {
            get => new ObservableCollection<Theme>(Themes.Where(t => t.IsSelected));
        }

        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Команды
        public ICommand ShowTasksCommand { get; set; }
        public ICommand ShowCalendarCommand { get; set; }
        public ICommand AddThemeCommand { get; set; }
        public ICommand DeleteSelectedThemesCommand { get; set; }

        // Методы команд
        private void ShowTasks(object parameter)
        {
            var themeIds = SelectedThemes.Select(t => t.ThemeId).ToList();
            CurrentView = new TaskListViewModel(themeIds);
        }

        private void ShowCalendar(object parameter)
        {
            // Реализация показа календаря
        }

        private void AddTheme(object parameter)
        {
            // Открытие диалогового окна для ввода имени темы
            AddThemeDialog dialog = new AddThemeDialog();
            if (dialog.ShowDialog() == true)
            {
                string newThemeName = dialog.ThemeName;

                // Проверка на существование темы с таким именем
                if (Themes.Any(t => t.ThemeName.Equals(newThemeName, System.StringComparison.OrdinalIgnoreCase)))
                {
                    // Уведомление пользователя
                    MessageBox.Show("Тема с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание новой темы
                Theme newTheme = new Theme { ThemeName = newThemeName };
                bool success = _themeService.AddTheme(newTheme, UserSession.CurrentUserId);

                if (success)
                {
                    Themes.Add(newTheme);
                    newTheme.PropertyChanged += Theme_PropertyChanged;
                }
                else
                {
                    // Уведомление пользователя о неудачной попытке добавления темы
                    MessageBox.Show("Не удалось добавить тему. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteSelectedThemes(object parameter)
        {
            var selectedThemes = SelectedThemes.ToList();
            var themeIds = selectedThemes.Select(t => t.ThemeId).ToList();
            _themeService.DeleteThemes(themeIds);

            foreach (var theme in selectedThemes)
            {
                Themes.Remove(theme);
            }

            // Обновляем команды
            OnPropertyChanged(nameof(SelectedThemes));
            (ShowTasksCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ShowCalendarCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteSelectedThemesCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // Проверки на возможность выполнения команд
        private bool CanShowContent(object parameter)
        {
            return SelectedThemes.Count > 0;
        }

        private bool CanDeleteThemes(object parameter)
        {
            return SelectedThemes.Count > 0;
        }

        // Обработка изменения свойств тем
        private void Theme_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Theme.IsSelected))
            {
                OnPropertyChanged(nameof(SelectedThemes));
                (ShowTasksCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (ShowCalendarCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteSelectedThemesCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // Обработка изменений в коллекции Themes
        private void Themes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Theme newTheme in e.NewItems)
                {
                    newTheme.PropertyChanged += Theme_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Theme oldTheme in e.OldItems)
                {
                    oldTheme.PropertyChanged -= Theme_PropertyChanged;
                }
            }
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
