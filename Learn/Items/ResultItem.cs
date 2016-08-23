using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn.Items
{
    public class ResultItem : INotifyPropertyChanged
    {
        private string questionString;
        private string questionImagePath;
        private int errorCount;
        private double answerSpeed;

        public string QuestionString
        {
            get
            {
                return questionString;
            }

            set
            {
                questionString = value;
                OnPropertyChanged();
            }
        }

        public string QuestionImagePath
        {
            get
            {
                return questionImagePath;
            }

            set
            {
                questionImagePath = value;
                OnPropertyChanged();
            }
        }

        public int ErrorCount
        {
            get
            {
                return errorCount;
            }

            set
            {
                errorCount = value;
                OnPropertyChanged();
            }
        }

        public double AnswerSpeed
        {
            get
            {
                return answerSpeed;
            }

            set
            {
                answerSpeed = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FullResult
    {
        public ObservableCollection<ResultItem> ResultList = new ObservableCollection<ResultItem>();

        // based on list dunno player max combo
        public int MaxCombo { get; set; }

    }

}
