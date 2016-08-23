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
    public class TestViewModel : INotifyPropertyChanged
    {
        private int totalQuestions;
        private int answeredQuestions;
        private int comboCount;
        private double answerSpeed;
        private string answer;

        public int TotalQuestions
        {
            get
            {
                return totalQuestions;
            }

            set
            {
                totalQuestions = value;
                OnPropertyChanged();
            }
        }

        public int AnsweredQuestions
        {
            get
            {
                return answeredQuestions;
            }

            set
            {
                answeredQuestions = value;
                OnPropertyChanged();
            }
        }

        public int ComboCount
        {
            get
            {
                return comboCount;
            }

            set
            {
                comboCount = value;
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

        public ObservableCollection<QuestionItem> QuestionList { get; set; }

        public string Answer
        {
            get
            {
                return answer;
            }

            set
            {
                answer = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {
            QuestionList = new ObservableCollection<QuestionItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
