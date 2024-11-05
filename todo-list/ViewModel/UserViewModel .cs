using System.ComponentModel;
using System.Runtime.CompilerServices;
using todo_list.Models;
using todo_list.Services;

namespace todo_list.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage; // Новое свойство для сообщений об ошибках
        private UserService _userService;

        public UserViewModel()
        {
            _userService = new UserService();
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                System.Diagnostics.Debug.WriteLine($"ViewModel Password set to: {_password}");
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool Login()
        {
            int userId = _userService.AuthenticateUser(Username, Password);
            if (userId > 0)
            {
                UserSession.CurrentUserId = userId;
                UserSession.CurrentUsername = Username;
                return true;
            }
            ErrorMessage = "Неверные учетные данные. Пожалуйста, попробуйте снова.";
            return false;
        }

        public bool Register()
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username и Password не могут быть пустыми.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Username не может быть пустыми.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password не может быть пустыми.";
                return false;
            }

            // Проверка на существование пользователя
            if (_userService.IsUsernameTaken(Username))
            {
                ErrorMessage = "Имя пользователя уже занято. Пожалуйста, выберите другое.";
                return false;
            }

            string error = _userService.RegisterUser(Username, Password);
            if (error == null)
            {
                ErrorMessage = null; // Очистить сообщение об ошибке при успешной регистрации
                return true;
            }
            else
            {
                ErrorMessage = $"Регистрация не удалась: {error}";
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
