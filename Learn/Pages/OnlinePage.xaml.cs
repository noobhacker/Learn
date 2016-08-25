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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var response = await WebAPI.GetBooksAsync();
                foreach(var book in response)
                {
                    vm.Books.Add(new Items.BookItem()
                    {
                        BookId = book.Id,
                        BookTitle = book.Title
                    });
                }
            }
            catch
            {
                MainPage.vm.Title = "No Internet Connection";
            }
            loading.IsActive = false;
        }

        private void booksGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
