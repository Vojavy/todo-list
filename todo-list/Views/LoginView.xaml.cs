// File: Views/LoginView.xaml.cs
using System.Windows;
using System.Windows.Controls;
using todo_list.ViewModels;

namespace todo_list.Views
{
    public partial class LoginView : UserControl
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel;

            // Подписываемся на событие успешного входа
            _viewModel.LoginSucceeded += OnLoginSucceeded;

            this.Unloaded += LoginView_Unloaded;
        }

        private void LoginView_Unloaded(object sender, RoutedEventArgs e)
        {
            // Отписываемся от события, чтобы избежать утечек памяти
            _viewModel.LoginSucceeded -= OnLoginSucceeded;
        }

        private void OnLoginSucceeded()
        {
            // Создаем и отображаем главное окно
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();

            // Закрываем текущее окно авторизации
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Content == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
