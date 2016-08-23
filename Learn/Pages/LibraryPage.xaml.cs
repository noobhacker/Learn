﻿using Learn.Items;
using Learn.Models;
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
    public sealed partial class LibraryFrame : Page
    {
        LibraryViewModel vm = new LibraryViewModel();
        public LibraryFrame()
        {
            this.InitializeComponent();
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
          this.Frame.Navigate(typeof(TestFrame),vm.Books[booksGV.SelectedIndex].BookId);
        }

        private void booksGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = booksGV.SelectedIndex;
            vm.BookTitle = vm.Books[index].BookTitle;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddBookFrame));
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //    if(booksGV.SelectedItems.Count != 0)
            //    {
            //        GlobalViewModel.Books.RemoveAt(booksGV.SelectedIndex);
            //        await IOClass.SaveBooks();
            //    }

            var db = new DatabaseContext();

            await db.SaveChangesAsync();
        }
    }

}