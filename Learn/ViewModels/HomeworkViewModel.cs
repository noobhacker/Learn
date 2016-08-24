using Learn.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ViewModels
{
    public class HomeworkViewModel : INotifyPropertyChanged
    {
        private string name;
        private DateTimeOffset dueDate;
        private int points;

        public ObservableCollection<HomeworkItem> Homeworks { get; set; }

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

        public HomeworkViewModel()
        {
            Homeworks = new ObservableCollection<HomeworkItem>();
            DueDate = DateTime.Now.AddDays(1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
