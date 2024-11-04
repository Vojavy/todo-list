// File: Services/IDialogService.cs
using System.Windows;

namespace todo_list.Services
{
    public interface IDialogService
    {
        string ShowInputDialog(string title, string prompt);
        void ShowMessage(string message, string caption, MessageBoxImage icon);
    }
}
