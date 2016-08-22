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

        //ObservableCollection<Backend.Book> BookList = new ObservableCollection<Backend.Book>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {


            //    if (await IOClass.LoadBooks()) // to make sure loadbooks is done before load to local binding
            //    {
            //        booksGV.ItemsSource = GlobalViewModel.Books;
            //        await IOClass.LoadStaticBooks();
            //    }
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            //    this.Frame.Navigate(typeof(TestFrame),GlobalViewModel.Books[booksGV.SelectedIndex]);
        }

    private void booksGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //    try
        //    {
        //        booktitleTB.Text = GlobalViewModel.Books[booksGV.SelectedIndex].BookTitle;
        //        bookdescriptionTB.Text = GlobalViewModel.Books[booksGV.SelectedIndex].Description;
        //    }
        //    catch { } //prevent null exception
    }

    private void addBtn_Click(object sender, RoutedEventArgs e)
    {
        //    this.Frame.Navigate(typeof(AddBookFrame));
    }

    private async void deleteBtn_Click(object sender, RoutedEventArgs e)
    {
        //    if(booksGV.SelectedItems.Count != 0)
        //    {
        //        GlobalViewModel.Books.RemoveAt(booksGV.SelectedIndex);
        //        await IOClass.SaveBooks();
        //    }
    }
}  

}
