using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
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
