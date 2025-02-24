using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlazorApp.Client.Models
{
    public class StyleContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        string color;
        public StyleContext()
        {
            color = "#ADD8E6";
        }
        public string BackgroundColor
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default)
       => PropertyChanged?.Invoke(this, new(propertyName));
    }
}
