// File: Services/DialogService.cs
using System.Windows;
using todo_list.Views;

namespace todo_list.Services
{
    public class DialogService : IDialogService
    {
        public string ShowInputDialog(string title, string prompt)
        {
            AddCategoryDialog dialog = new AddCategoryDialog
            {
                Title = title,
                Owner = Application.Current.MainWindow
            };
            if (dialog.ShowDialog() == true)
            {
                return dialog.CategoryName;
            }
            return null;
        }

        public void ShowMessage(string message, string caption, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, icon);
        }
    }
}
