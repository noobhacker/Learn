using Learn.Items;
using Learn.Models;
using Learn.ViewModels;
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
    public sealed partial class HomeworkPage : Page
    {
        HomeworkViewModel vm = new HomeworkViewModel();
        public HomeworkPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var db = new DatabaseContext();
            foreach (var homework in db.Homeworks)
            {
                vm.Homeworks.Add(new HomeworkItem()
                {
                    Name = homework.Name,
                    Points = homework.Points,
                    DueDate = homework.DueDate,
                    Id = homework.Id // bring id to do operations
                });
            }
        }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var db = new DatabaseContext();
            var result = db.Homeworks.Add(new Homework()
            {
                Name = vm.Name,
                DueDate = vm.DueDate.Date,
                Points = vm.Points
            });
            await db.SaveChangesAsync();

            // update ui but don't want refresh whole
            vm.Homeworks.Insert(0, new HomeworkItem()
            {
                Name = vm.Name,
                DueDate = vm.DueDate.Date,
                Points = vm.Points,
                Id = result.Entity.Id
            });
        }

        private async void forfeitBtn_Click(object sender, RoutedEventArgs e)
        {
            //        if(homeworksLV.SelectedItems.Count != 0)
            //        {
            //            GlobalViewModel.UserHomework.RemoveAt(homeworksLV.SelectedIndex);
            //            await IOClass.SaveHomework();
            //        }
        }

        private async void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            //        if(homeworksLV.SelectedItems.Count != 0)
            //        {

            //            // calculate bonus for finish earlier

            //            double multiplier = 1;
            //            TimeSpan extra;

            //            try
            //            {
            //                extra = GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].DueDate.Subtract(DateTime.Now);
            //                for (int i = 0; i < extra.Days; i++)
            //                    multiplier += 0.01;
            //            }
            //            catch { }

            //            int points = Convert.ToInt32(GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].Points * multiplier);

            //            GlobalViewModel.UserProfile.HomeworkEXP += points;
            //            // seperate homeworkexp with currentexp because homeworkexp just used to draw graph
            //            GlobalViewModel.UserProfile.CurrentExp += points;
            //            GlobalViewModel.UserProfile.CheckIfLevelUp();

            //            GlobalViewModel.UserProfile.Gold += points;
            //            GlobalViewModel.UserProfile.Cash += points / 100;

            //            GlobalViewModel.UserActivity.Add(new Backend.Activity()
            //            {
            //                ActionDate = DateTime.Now,
            //                ActionDescription = GlobalViewModel.UserHomework[homeworksLV.SelectedIndex].HomeworkName,
            //                ActionName = "Done Homework",
            //                ActionPoints = points + " (+" + extra.Days + "%)"

            //            });

            //            GlobalViewModel.UserHomework.RemoveAt(homeworksLV.SelectedIndex);

            //            await IOClass.SaveProfile();
            //            await IOClass.SaveHomework();
            //            await IOClass.SaveActivity();
            //        }
            //    }
        }

        private void showAddBarBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowAddBar.Begin();
        }

        private void homeworksLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAddBar.Begin();
        }
    }
}