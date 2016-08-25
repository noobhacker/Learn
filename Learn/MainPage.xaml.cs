using Learn.Models;
using Learn.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPageViewModel vm = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();      
            this.DataContext = vm;   
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            while (listBox.Items.Count == 0)
                await Task.Delay(100);

            listBox.SelectedIndex = 0;

            var db = new DatabaseContext();
            var user = db.Users.First();
            vm.Exp = user.CurrentExp;
            vm.LevelUpExp = user.NextLevelExp;
            vm.ProfileName = user.Name;
            vm.Skin = user.SkinColor;
            vm.Level = user.Level;

        }

        private void hambugerBtn_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;
            var item = vm.HamburgerMenuItems[index];

            frame.Navigate(item.TargetFrame);
            vm.Title = item.Text.ToUpper();

            splitView.IsPaneOpen = false;
        }


    }
}
