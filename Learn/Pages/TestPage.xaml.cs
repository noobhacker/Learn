using Learn.Helpers;
using Learn.Items;
using Learn.Models;
using Learn.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
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
    public sealed partial class TestPage : Page
    {
        TestViewModel vm = new TestViewModel();
        DispatcherTimer answerspeedDT = new DispatcherTimer();

        public TestPage()
        {
            this.InitializeComponent();

            answerspeedDT.Interval = new TimeSpan(0, 0, 0, 0, 1);
            answerspeedDT.Tick += answerspeedDT_Tick;

        }

        private bool checkAnswer(string answer)
        {
            if (vm.QuestionList.Count != 0)
            {
                if (vm.QuestionList[0].AnswerString == answer)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void answerspeedDT_Tick(object sender, object e)
        {
            vm.AnswerSpeed += 0.031;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.SourcePageType == typeof(LibraryPage))
            {
                var db = new DatabaseContext();
                var id = Convert.ToInt32(e.Parameter);
                foreach (var question in db.Questions.Where(x => x.BookId == id))
                {
                    vm.QuestionList.Add(new QuestionItem()
                    {
                        AnswerString = question.AnswerString,
                        QuestionImageId = question.QuestionImageId,
                        QuestionString = question.QuestionString
                    });
                }
            }
            // else from internet

            //// add them in before randomize so it will maintain
            //// sequence in result screen
            for (int i = 0; i < vm.QuestionList.Count; i++)
            {

                try
                {
                    await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images");
                }
                catch { }

                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");

                vm.QuestionList[i].QuestionImagePath = folder.Path + "\\" +
                    vm.QuestionList[i].QuestionImageId;

                AnsweredResult.ResultList.Add(new ResultItem()
                {
                    QuestionImagePath = vm.QuestionList[i].QuestionImagePath,
                    QuestionString = Convert.ToString(vm.QuestionList[i].QuestionString)
                });
            }

            vm.QuestionList.Randomize();
            answerspeedDT.Start();
            answerTextBox.Focus(FocusState.Keyboard);
        }

        FullResult AnsweredResult = new FullResult();

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
                    if ((AnsweredResult.ResultList[i].QuestionString ==
                        vm.QuestionList[0].QuestionString &&

                        vm.QuestionList[0].QuestionString != null) || //else this will always trigger
                        (AnsweredResult.ResultList[i].QuestionImagePath ==
                        vm.QuestionList[0].QuestionImagePath &&

                        vm.QuestionList[0].QuestionImagePath != null) &&
                        vm.QuestionList[0].QuestionImageId != 0
                        //&&
                        //AnsweredResult.ResultList[i].AnswerSpeed == null //to prevent re-insert speed to answered question
                        )
                    {
                        // if correct answer will remove the first item
                        if (checkAnswer(vm.Answer))
                        {

                            // adds the combo count
                            vm.ComboCount++;
                            vm.AnsweredQuestions++;

                            // equal to AnswerSpeed so no need deal with 0.000000001 problem

                            if (answerTextBox.Text.Contains(" "))
                            {
                                AnsweredResult.ResultList[i].AnswerSpeed =
                                Math.Round(vm.AnswerSpeed / answerTextBox.Text.Length, 2);
                            }
                            else
                            {
                                AnsweredResult.ResultList[i].AnswerSpeed = vm.AnswerSpeed;
                            }


                            // remove here , if remove in Book class will exception
                            vm.QuestionList.RemoveFirstQuestion();

                            // end of game
                            if (vm.QuestionList.Count == 0)
                            {
                                answerspeedDT.Stop();
                                AnsweredResult.MaxCombo = vm.ComboCount;
                                this.Frame.Navigate(typeof(ResultPage), AnsweredResult);
                            }

                        }
                        // here goes if the answer is wrong
                        else
                        {
                            errorGrid.Visibility = Visibility.Visible;
                            errorImage.Source =
                                new BitmapImage(new Uri(vm.QuestionList[0].QuestionImagePath));
                            errorTB.Text =
                                Convert.ToString(vm.QuestionList[0].QuestionString) + " " +
                                vm.QuestionList[0].AnswerString[0];

                            // stop timer before wait async
                            answerspeedDT.Stop();

                            // wait for 1 second async
                            // async will cause questionlist randomize and timer keep running
                            await Task.Delay(TimeSpan.FromSeconds(1));

                            // start after stopped the timer just now
                            answerspeedDT.Start();

                            errorGrid.Visibility = Visibility.Collapsed;

                            AnsweredResult.ResultList[i].ErrorCount++;
                            vm.ComboCount = 0;
                            vm.QuestionList.Randomize();
                        }

                        // if no break will continue for loop even only enter answer once
                        // break after found the same questionstring in result list
                        break;
                    }
                }

                // no matter correct or wrong will reset timer
                vm.AnswerSpeed = 0;
                answerTextBox.Text = "";

            }
        }
    }
}