using System.Windows;
using todo_list.Database;
using todo_list.Views;

namespace todo_list
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseInitializer.Initialize();

            // Запуск окна входа
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DatabaseContext.Instance.Dispose();
            base.OnExit(e);
        }
    }
}
