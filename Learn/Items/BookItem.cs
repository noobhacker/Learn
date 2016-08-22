using Learn.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Learn.Items
{
    public class BookItem
    {

        public enum QuestionTypes { MCQ, StringQuestion, ImageQuestion, VoiceQuestion}

        public string BookTitle { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public ObservableCollection<QuestionItem> QuestionList = new ObservableCollection<QuestionItem>() ;
        public bool CheckAnswer(string answer)
        { 
            bool result = true;


            // the for loop and AnswerString[i] is for checking each answer
            // matches one of the answer from the question or not 
            if (QuestionList.Count != 0)
            {
                for (int i = 0; i < QuestionList[0].AnswerString.Length; i++)
                {
                    if (QuestionList[0].AnswerString[i] == answer)
                    {

                        //CANNOT REMOVE FIRST BECAUSE AFTER CHECKANSWER
                        //STILL HAVE A LOT OF THING NEED THE QUESTION i GOING TO REMOVE

                        //QuestionList.RemoveAt(0); //remove first one

                        // MUST BREAK AFTER REMOVE OR WILL LOOP BACK UPSIDE
                        // SAY NOTHING LEFT IN QUESTIONLIST
                        break;
                    }
                    else if (QuestionList[0].AnswerString[i] != answer)
                    {
                        result = false;

                        //Randomize();
                    }
                }
            }
            return result;
        }
        public void Randomize()
        {
            QuestionList.Shuffle();
        }

        public void RemoveFirstQuestion()
        {
            QuestionList.RemoveAt(0);
        }


    }
    
}

