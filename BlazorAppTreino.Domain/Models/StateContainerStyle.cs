using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{
   public class StateContainerStyle
    {
        string? _color;

        public string? Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotificationHasChanged();
            }
        }
        public StateContainerStyle()
        {
            _color = "#ADD8E6";
        }

        public Action? Notification;

        private void NotificationHasChanged()
        {
            Notification?.Invoke();
        }

    }
}
