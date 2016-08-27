using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.Items
{
    public class HomeworkItem : INotifyPropertyChanged
    {
        private string name;
        private DateTimeOffset dueDate;
        private int points;
        private int id;

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

        public DateTimeOffset DueDate
        {
            get
            {
                return dueDate;
            }

            set
            {
                dueDate = value;
                OnPropertyChanged();
            }
        }

        public int Points
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

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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
