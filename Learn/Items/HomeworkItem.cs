using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.Items
{
    public class HomeworkItem : INotifyPropertyChanged
    {
        private string homeworkName;
        private DateTime dueDate;
        private int points;

        public string HomeworkName
        {
            get
            {
                return homeworkName;
            }

            set
            {
                homeworkName = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDate
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
