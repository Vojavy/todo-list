// File: ViewModels/LoginViewModel.cs
using System;
using System.Windows;
using System.Windows.Input;
using todo_list.Commands;
using todo_list.Models;
using todo_list.Services;
using todo_list.Views;

namespace todo_list.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        // Событие для уведомления представления об успешном входе
        public event Action LoginSucceeded;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object parameter)
        {
            bool isValidUser = _userService.ValidateUser(Username, Password);
            if (isValidUser)
            {
                // Уведомляем представление об успешном входе
                LoginSucceeded?.Invoke();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register(object parameter)
        {
            bool isRegistered = _userService.RegisterUser(new User { Username = Username, Password = Password });
            if (isRegistered)
            {
                MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти в систему.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Регистрация не удалась. Возможно, имя пользователя уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
