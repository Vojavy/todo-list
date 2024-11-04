// File: Views/AddCategoryDialog.xaml.cs
using System.Windows;

namespace todo_list.Views
{
    public partial class AddCategoryDialog : Window
    {
        public string CategoryName { get; private set; }

        public AddCategoryDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CategoryNameTextBox.Text))
            {
                CategoryName = CategoryNameTextBox.Text.Trim();
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a category name.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
