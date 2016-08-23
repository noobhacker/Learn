using Learn.Items;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.ViewModels
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
        private string bookTitle;
        private string textQuestions;
        private string imagePath;
        private string imageAnswer;

        public string BookTitle
        {
            get
            {
                return bookTitle;
            }

            set
            {
                bookTitle = value;
                OnPropertyChanged();
            }
        }

        public string TextQuestions
        {
            get
            {
                return textQuestions;
            }

            set
            {
                textQuestions = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        public string ImageAnswer
        {
            get
            {
                return imageAnswer;
            }

            set
            {
                imageAnswer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<QuestionItem> QuestionsList { get; set; }

        public AddBookViewModel()
        {
            QuestionsList = new ObservableCollection<QuestionItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
