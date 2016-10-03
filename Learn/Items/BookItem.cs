using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.Items
{
    public class BookItem : INotifyPropertyChanged
    {        
        private string bookTitle;
        private int bookId;
        private string textColor;

        public string BookTitle
        {
            get
            {
                return bookTitle;
            }

            set
            {
                bookTitle = value;
            }
        }

        public int BookId
        {
            get
            {
                return bookId;
            }

            set
            {
                bookId = value;
            }
        }

        public string TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
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

