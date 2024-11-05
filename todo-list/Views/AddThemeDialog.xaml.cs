using System.Windows;

namespace todo_list.Views
{
    public partial class AddThemeDialog : Window
    {
        public string ThemeName { get; private set; }

        public AddThemeDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ThemeNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название темы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ThemeName = ThemeNameTextBox.Text.Trim();
            this.DialogResult = true;
        }
    }
}
