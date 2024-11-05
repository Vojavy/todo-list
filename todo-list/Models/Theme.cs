using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace todo_list.Models
{
    public class Theme : INotifyPropertyChanged
    {
        private bool _isSelected;

        public int ThemeId { get; set; }
        public string ThemeName { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    // Уведомляем ViewModel об изменении выбранных тем
                    OnPropertyChanged(nameof(IsSelected));
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
