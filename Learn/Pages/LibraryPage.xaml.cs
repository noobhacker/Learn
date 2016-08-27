using Learn.Helpers;
using Learn.Items;
using Learn.Models;
using Learn.Pages;
using Learn.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class LibraryPage : Page
    {
        LibraryViewModel vm = new LibraryViewModel();
        public LibraryPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var db = new DatabaseContext();
            foreach (var book in db.Books)
            {
                vm.Books.Add(new BookItem()
                {
                    BookId = book.Id,
                    BookTitle = book.Title
                });
            }

        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
             Frame.Navigate(typeof(TestPage),vm.Books[booksGV.SelectedIndex].BookId);
        }

        private void booksGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as GridView).SelectedIndex == -1)
            {
                editBtn.Visibility = Visibility.Collapsed;
                deleteBtn.Visibility = Visibility.Collapsed;
                readBtn.Visibility = Visibility.Collapsed;
                testBtn.Visibility = Visibility.Collapsed;
                shareBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                editBtn.Visibility = Visibility.Visible;
                deleteBtn.Visibility = Visibility.Visible;
                readBtn.Visibility = Visibility.Visible;
                testBtn.Visibility = Visibility.Visible;
                shareBtn.Visibility = Visibility.Visible;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddBookPage));
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (await DialogHelper.ShowYesNoDialogAsync("Are you sure?") == 0)
            {
                var index = booksGV.SelectedIndex;
                var db = new DatabaseContext();

                var bookId = vm.Books[index].BookId;
                db.Remove(db.Books.FirstOrDefault(x => x.Id == bookId));
                foreach(var question in db.Questions.Where(x => x.BookId == bookId))
                {
                    db.Remove(question);
                }

                await db.SaveChangesAsync();

                vm.Books.RemoveAt(index);
            }          
        }

        private async void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = booksGV.SelectedIndex;
            var db = new DatabaseContext();
            if(db.Questions.FirstOrDefault(x=>x.BookId == vm.Books[index].BookId).QuestionString == null)
            {
                await DialogHelper.ShowDialogAsync("Books with image questions can't be shared online");
            }
            else
            {
                try
                {
                    loading.IsActive = true;
                    shareBtn.IsEnabled = false;

                    await WebAPI.UploadBookAsync(vm.Books[index].BookId);
                    await DialogHelper.ShowDialogAsync("Book shared online!");
                    MainPage.vm.Title = "Online";
                    Frame.Navigate(typeof(OnlinePage));

                    loading.IsActive = false;
                    shareBtn.IsEnabled = true;
                }
                catch
                {
                    await DialogHelper.ShowDialogAsync("Something went wrong");
                }
            }
        }

        private void readBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = booksGV.SelectedIndex;
            Frame.Navigate(typeof(ReadPage), vm.Books[index].BookId);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = booksGV.SelectedIndex;
            Frame.Navigate(typeof(AddBookPage), vm.Books[index].BookId);
        }

    }
}
