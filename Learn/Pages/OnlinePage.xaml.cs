﻿using Learn.Models;
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

namespace Learn.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OnlinePage : Page
    {
        OnlineViewModel vm = new OnlineViewModel();
        public OnlinePage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private void displayFilteredBooks()
        {
            vm.FilteredBooks.Clear();
            foreach(var item in vm.Books.Where(x=>
            vm.SearchBoxText == null || 
            vm.SearchBoxText == "" || 
            x.BookTitle.Contains(vm.SearchBoxText)))
            {
                vm.FilteredBooks.Add(item);
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var db = new DatabaseContext();
            try
            {
                var response = await WebAPI.GetBooksAsync();
                foreach(var book in response)
                {
                    vm.Books.Add(new Items.BookItem()
                    {
                        BookId = book.Id,
                        BookTitle = book.Title,
                        TextColor = db.Books.Any(x => x.Title == book.Title) ? "#888888" : ""
                    });
                }
            }
            catch
            {
                MainPage.vm.Title = "No Internet Connection";
            }
            loading.IsActive = false;
            displayFilteredBooks();
        }

        private void booksGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as GridView).SelectedIndex != -1)        
                readBtn.Visibility = Visibility.Visible;       
            else         
                readBtn.Visibility = Visibility.Collapsed;      
        }

        private async void readBtn_Click(object sender, RoutedEventArgs e)
        {
            loading.IsActive = true;

            var index = booksGV.SelectedIndex;
            var id = await WebAPI.DownloadBookByIdAsync(vm.Books[index].BookId);

            Frame.Navigate(typeof(ReadPage),id);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            displayFilteredBooks();
        }
    }
}
