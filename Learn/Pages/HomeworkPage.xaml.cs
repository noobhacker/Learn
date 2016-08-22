using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeworkFrame : Page
    {
        public HomeworkFrame()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //var listviewitems = new List<HomeworkListviewItem>();
            //listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Final Year Project", DueDate = "12 May 2016", Points = 13500 });
            //listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Calculus 5 questions", DueDate = "12 May 2017", Points = 200 });
            //listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Computer Architecture tutorial 5", DueDate = "12 May 2018", Points = 500 });

            await IOClass.LoadHomework();

            homeworksLV.ItemsSource = GlobalViewModel.UserHomework;
            
        }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {

            GlobalViewModel.UserHomework.Add(new Backend.Homework()
            {
                DueDate = duedateDP.Date.DateTime,
                HomeworkName = homeworknameTB.Text,
                Points = Convert.ToInt32(pointsTB.Text)
            });
            await IOClass.SaveHomework();
        }

        private async void forfeitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(homeworksLV.SelectedItems.Count != 0)
            {
                GlobalViewModel.UserHomework.RemoveAt(homeworksLV.SelectedIndex);
                await IOClass.SaveHomework();
            }
        }

        private async void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if(homeworksLV.SelectedItems.Count != 0)
            {

                // calculate bonus for finish earlier

                double multiplier = 1;
                TimeSpan extra;

                try
                {
                    extra = GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].DueDate.Subtract(DateTime.Now);
                    for (int i = 0; i < extra.Days; i++)
                        multiplier += 0.01;
                }
                catch { }

                int points = Convert.ToInt32(GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].Points * multiplier);

                GlobalViewModel.UserProfile.HomeworkEXP += points;
                // seperate homeworkexp with currentexp because homeworkexp just used to draw graph
                GlobalViewModel.UserProfile.CurrentExp += points;
                GlobalViewModel.UserProfile.CheckIfLevelUp();
              
                GlobalViewModel.UserProfile.Gold += points;
                GlobalViewModel.UserProfile.Cash += points / 100;

                GlobalViewModel.UserActivity.Add(new Backend.Activity()
                {
                    ActionDate = DateTime.Now,
                    ActionDescription = GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].HomeworkName,
                    ActionName = "Done Homework",
                    ActionPoints = points + " (+" + extra.Days + "%)"

                });

                GlobalViewModel.UserHomework.RemoveAt(homeworksLV.SelectedIndex);

                await IOClass.SaveProfile();
                await IOClass.SaveHomework();
                await IOClass.SaveActivity();
            }
        }
    }

    public class HomeworkListviewItem
    {
        public string HomeworkName { get; set; }
        public string DueDate { get; set; }  //use string so cleaner inside xaml binding
        public int Points { get; set; }
    }
}
