using System.Windows;
using todo_list.ViewModels;

namespace todo_list.Views
{
    public partial class LoginWindow : Window
    {
        private UserViewModel _userViewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
            DataContext = _userViewModel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userViewModel.Login())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Сообщение об ошибке уже устанавливается в ViewModel и отображается через привязку
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userViewModel.Register())
            {
                MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти.", "Регистрация успешна", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Сообщение об ошибке уже устанавливается в ViewModel и отображается через привязку
            }
        }
    }
}
