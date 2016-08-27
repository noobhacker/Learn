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
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        ReadViewModel vm = new ReadViewModel();
        DispatcherTimer dt = new DispatcherTimer();

        public ReadPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += (sender, e) =>
            {
                Frame.Navigate(typeof(LibraryPage));
            };

            dt.Interval = new TimeSpan(0, 1, 0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private async void Dt_Tick(object sender, object e)
        {
            var db = new DatabaseContext();
            db.Users.First().CurrentExp += 100;
            db.Users.First().ReadingEXP += 100;

            await db.SaveChangesAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            dt.Stop();
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var db = new DatabaseContext();
            var bookId = Convert.ToInt32(e.Parameter);
            
            foreach(var question in db.Questions.Where(x=>x.BookId == bookId))
            {
                vm.QuestionsList.Add(new QuestionItem()
                {
                    QuestionString = question.QuestionString,
                    AnswerString = question.AnswerString,
                    QuestionImageId = question.QuestionImageId,
                    QuestionImagePath = ApplicationData.Current.LocalFolder.Path + "\\Images\\" + question.QuestionImageId,
                    QuestionImageVisibility = question.QuestionString == null ? Visibility.Visible : Visibility.Collapsed
                });
            }
        }
    }
}
