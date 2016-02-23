using Learn.Backend;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn
{
    class GlobalViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<Book> Books;
        public static ObservableCollection<Book> StaticBooks;
        public static Backend.Profile UserProfile;

        private static Skin userSkin;

        public Skin UserSkin
        {
            get
            {
                return userSkin;
            }
            set
            {
                if (value != userSkin)
                {
                    userSkin = value;
                    OnPropertyChanged();
                }

            }
        }

        //public static Backend.Voice UserVoice;

        public static ObservableCollection<Activity> UserActivity; // = new ObservableCollection<Activity>();

        public static ObservableCollection<Homework> UserHomework; // = new ObservableCollection<Homework>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
