using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Learn.Items
{
    public class QuestionItem : INotifyPropertyChanged
    {
        private string questionString;
        private string questionImagePath;
        private int questionImageId;
        private Visibility questionImageVisibility;
        private string answerString;

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

        public int QuestionImageId
        {
            get
            {
                return questionImageId;
            }

            set
            {
                questionImageId = value;
                OnPropertyChanged();
            }
        }

        public Visibility QuestionImageVisibility
        {
            get
            {
                return questionImageVisibility;
            }

            set
            {
                questionImageVisibility = value;
                OnPropertyChanged();
            }
        }

        public string AnswerString
        {
            get
            {
                return answerString;
            }

            set
            {
                answerString = value;
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
