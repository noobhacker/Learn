using Learn.Items;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestFrame : Page, INotifyPropertyChanged
    {

        //INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        DispatcherTimer answerspeedDT = new DispatcherTimer();

        public TestFrame()
        {
            this.InitializeComponent();

            answerspeedDT.Interval = new TimeSpan(0, 0, 0, 0, 1);
            answerspeedDT.Tick += answerspeedDT_Tick;
            
        }

        private void answerspeedDT_Tick(object sender, object e)
        {
            dblAnswerSpeed += 0.031;
            AnswerSpeed = dblAnswerSpeed.ToString("0.000");
        }

        BookItem QuestioningBook;
        FullResult AnsweredResult = new FullResult();

        #region bindings
        int totalQuestions;
        int TotalQuestions
        {
            get { return totalQuestions; }
            set
            {               
                totalQuestions = value;
                NotifyPropertyChanged("TotalQuestions");
            }
        }
        

        int answeredQuestions;
        int AnsweredQuestions
        {
            get { return answeredQuestions; }
            set
            {
                answeredQuestions = value;
                NotifyPropertyChanged("AnsweredQuestions");
            }
        }

        int comboCount;
        int ComboCount
        {
            get { return comboCount; }
            set
            {
                comboCount = value;
                NotifyPropertyChanged("ComboCount");
            }

        }

        //directly bind double AnswerSpeed will become 0.00000000001 inaccuracy
        double dblAnswerSpeed = 0;
        string answerSpeed;
        string AnswerSpeed
        {
            get { return answerSpeed; }
            set
            {
                answerSpeed = value;
                NotifyPropertyChanged("AnswerSpeed");
            }
        }

        #endregion

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            QuestioningBook = (BookItem)e.Parameter;
            

            // add them in before randomize so it will maintain
            // sequence in result screen
            for (int i = 0; i < QuestioningBook.QuestionList.Count; i++)
            {

                try
                {
                    await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images");
                }
                catch { }

                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");

                QuestioningBook.QuestionList[i].QuestionImagePath = folder.Path + "\\" +
                    QuestioningBook.QuestionList[i].QuestionImageID;

                //AnsweredResult.ResultList.Add(new ResultItem()
                //{
                //    QuestionImagePath = QuestioningBook.QuestionList[i].QuestionImagePath ,
                //    QuestionString = Convert.ToString(QuestioningBook.QuestionList[i].QuestionString )
                //});
            }

            QuestioningBook.Randomize();

            QuestionsGV.ItemsSource = QuestioningBook.QuestionList;

            TotalQuestions = QuestioningBook.QuestionList.Count;
            answeredQuestions = 0;
            comboCount = 0;
            answerspeedDT.Start();



            answerTextBox.Focus(FocusState.Keyboard );
        }
        

        private async void answerTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && answerTextBox.Text != "")
            {

                // I put for loop then check for answer is because
                // answer is right or wrong also need to loop through to find the position
                // in result list

                // wrong need to add error count, right need to key in answertime

                // loop through result list to find the same questionstring in resultlist with 
                // the first questionstring in QuestioningBooks
                for (int i = 0; i < AnsweredResult.ResultList.Count; i++)
                {

                    // if the questionstring in resultlist equals to first questionstring in UI
                    if (
                        (AnsweredResult.ResultList[i].QuestionString ==
                        QuestioningBook.QuestionList[0].QuestionString &&

                        QuestioningBook.QuestionList[0].QuestionString != null ) //else this will always trigger
                        ||
                        (AnsweredResult.ResultList[i].QuestionImagePath == 
                        QuestioningBook.QuestionList[0].QuestionImagePath && 

                        QuestioningBook.QuestionList[0].QuestionImagePath != null) &&
                        QuestioningBook.QuestionList[0].QuestionImageID != 0
                        &&
                        AnsweredResult.ResultList[i].AnswerSpeed == null //to prevent re-insert speed to answered question
                        )
                    {
                        // if correct answer will remove the first item
                        if (QuestioningBook.CheckAnswer(answerTextBox.Text))
                        {

                            // adds the combo count
                            ComboCount++;
                            AnsweredQuestions++;

                            // equal to AnswerSpeed so no need deal with 0.000000001 problem

                            if (answerTextBox.Text.Contains(" "))
                            {
                                AnsweredResult.ResultList[i].AnswerSpeed = Convert.ToString(
                                Math.Round((Convert.ToDouble(AnswerSpeed) / answerTextBox.Text.Length), 2));
                            }
                            else
                            {
                                AnsweredResult.ResultList[i].AnswerSpeed = AnswerSpeed;
                            }


                            // remove here , if remove in Book class will exception
                            QuestioningBook.RemoveFirstQuestion();

                            // end of game
                            if (QuestioningBook.QuestionList.Count == 0)
                            {
                                answerspeedDT.Stop();
                                AnsweredResult.MaxCombo = ComboCount;
                                this.Frame.Navigate(typeof(ResultFrame), AnsweredResult);
                            }
                            

                        }
                        // here goes if the answer is wrong
                        else
                        {
                            errorGrid.Visibility = Visibility.Visible;
                            errorImage.Source = 
                                new BitmapImage(new Uri(QuestioningBook.QuestionList[0].QuestionImagePath));
                            errorTB.Text =
                                Convert.ToString(QuestioningBook.QuestionList[0].QuestionString) + " " +
                                QuestioningBook.QuestionList[0].AnswerString[0];

                            // stop timer before wait async
                            answerspeedDT.Stop();

                            // wait for 1 second async
                            // async will cause questionlist randomize and timer keep running
                            await Task.Delay(TimeSpan.FromSeconds(1));

                            // start after stopped the timer just now
                            answerspeedDT.Start();

                            errorGrid.Visibility = Visibility.Collapsed;

                            AnsweredResult.ResultList[i].ErrorCount++;
                            comboCount = 0;
                            QuestioningBook.Randomize();
                                                        
                        }

                        // if no break will continue for loop even only enter answer once
                        // break after found the same questionstring in result list
                        break;
                    }
                }

                // no matter correct or wrong will reset timer
                dblAnswerSpeed = 0;
                answerTextBox.Text = "";

            }
        }
    }
}
