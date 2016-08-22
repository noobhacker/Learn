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
    public sealed partial class LibraryFrame : Page
    {
        public LibraryFrame()
        {
            this.InitializeComponent();
        }
         List<Backend.Book> binding = new List<Backend.Book>();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
                        
            binding.Add(new Backend.Book() { BookTitle = "A Book" });
            binding.Add(new Backend.Book() { BookTitle = "Another Book" });
            binding.Add(new Backend.Book() { BookTitle = "Another Book with longer title" });
            binding.Add(new Backend.Book() { BookTitle = "More Books" });

            booksGV.ItemsSource = binding;
        }
    }  

}
