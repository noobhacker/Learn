using Learn.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Learn.Items
{
    public class BookItem : INotifyPropertyChanged
    {

        //public enum QuestionTypes {StringQuestion, ImageQuestion}
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

        //public QuestionTypes QuestionType { get; set; }

        //public ObservableCollection<QuestionItem> QuestionList = new ObservableCollection<QuestionItem>() ;
        //public bool CheckAnswer(string answer)
        //{ 
        //    bool result = true;


        //    // the for loop and AnswerString[i] is for checking each answer
        //    // matches one of the answer from the question or not 
        //    if (QuestionList.Count != 0)
        //    {
        //        for (int i = 0; i < QuestionList[0].AnswerString.Length; i++)
        //        {
        //            if (QuestionList[0].AnswerString[i] == answer)
        //            {

        //                //CANNOT REMOVE FIRST BECAUSE AFTER CHECKANSWER
        //                //STILL HAVE A LOT OF THING NEED THE QUESTION i GOING TO REMOVE

        //                //QuestionList.RemoveAt(0); //remove first one

        //                // MUST BREAK AFTER REMOVE OR WILL LOOP BACK UPSIDE
        //                // SAY NOTHING LEFT IN QUESTIONLIST
        //                break;
        //            }
        //            else if (QuestionList[0].AnswerString[i] != answer)
        //            {
        //                result = false;

        //                //Randomize();
        //            }
        //        }
        //    }
        //    return result;
        //}
        //public void Randomize()
        //{
        //    QuestionList.Shuffle();
        //}

        //public void RemoveFirstQuestion()
        //{
        //    QuestionList.RemoveAt(0);
        //}


    }

}

