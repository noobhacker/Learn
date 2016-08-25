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
            var index = (sender as ListView).SelectedIndex;
            if (homeworksLV.SelectedItems.Count != 0)
            {
                var db = new DatabaseContext();
                db.Homeworks.Remove(db.Homeworks.First(x => x.Id == vm.Homeworks[index].Id));
                await db.SaveChangesAsync();

                vm.Homeworks.RemoveAt(index);
            }
        }

        private async void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (homeworksLV.SelectedItems.Count != 0)
            {
                var db = new DatabaseContext();
                var index = homeworksLV.SelectedIndex;
                // cant do this will miss track which property modified
                //var user = db.Users.First();
                double multiplier = 1;
                TimeSpan extra;

                try
                {
                    extra = vm.Homeworks[index].DueDate.Subtract(DateTime.Now);
                    for (int i = 0; i < extra.Days; i++)
                        multiplier += 0.01;
                }
                catch { }

                int points = Convert.ToInt32(vm.Homeworks[index].Points * multiplier);

                db.Users.First().HomeworkEXP += points;
                // seperate homeworkexp with currentexp because homeworkexp just used to draw graph
                db.Users.First().CurrentExp += points;

                db.Users.First().Gold += points;

                db.Activities.Add(new Activity()
                {
                    Date = DateTime.Now,
                    Description = vm.Homeworks[index].Name,
                    Name = "Done Homework",
                    Points = points + " (+" + extra.Days + "%)"
                });

                db.Homeworks.Remove(db.Homeworks.First(x => x.Id == vm.Homeworks[index].Id));
                vm.Homeworks.RemoveAt(index);

                MainPage.vm.Exp += points;
                MainPage.vm.CheckIfLevelUp();

                await db.SaveChangesAsync();
            }
        }

        private void showAddBarBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowAddBar.Begin();
        }

        private void homeworksLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(addBar.Height != 0)
                HideAddBar.Begin();
        }
    }
}