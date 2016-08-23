using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.Items
{
    public class BookItem : INotifyPropertyChanged
    {        
        private string bookTitle;
        private int bookId;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }    

    }

}

