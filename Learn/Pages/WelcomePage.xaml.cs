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

namespace Learn.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        WelcomeViewModel vm = new WelcomeViewModel();
        public WelcomePage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private async void initialize(string name)
        {
            doneBtn.IsEnabled = false;
            doneBtn.Content = "Initializing";

            var db = new DatabaseContext();
            db.Users.Add(new User()
            {
                Name = name,
                Level = 1,
                LevelUpExp=100,
                Gold=50000
            });
            await db.SaveChangesAsync();
            Frame.Navigate(typeof(MainPage));
        }

        private void nameTB_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (vm.Name == "")
            {
                doneBtn.IsEnabled = false;
                return;
            }
            else
            {
                doneBtn.IsEnabled = true;
            }

            if (e.Key == Windows.System.VirtualKey.Enter)
                initialize(vm.Name);
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            initialize(vm.Name);
        }
    }
}
