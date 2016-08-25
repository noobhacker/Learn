using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Items
{
    public class ActivityItem : INotifyPropertyChanged
    {
        private DateTimeOffset date;
        private string name;
        private string description;
        private string points;

        public DateTimeOffset Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public string Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
