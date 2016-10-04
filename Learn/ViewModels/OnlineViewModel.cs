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
    public class OnlineViewModel : INotifyPropertyChanged
    {
        private string searchBoxText;
        public ObservableCollection<BookItem> Books { get; set; }
        public ObservableCollection<BookItem> FilteredBooks { get; set; }

        public string SearchBoxText
        {
            get
            {
                return searchBoxText;
            }

            set
            {
                searchBoxText = value;
                OnPropertyChanged();
            }
        }

        public OnlineViewModel()
        {
            Books = new ObservableCollection<BookItem>();
            FilteredBooks = new ObservableCollection<BookItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
