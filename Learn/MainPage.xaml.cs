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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            GlobalViewModel.UserProfile = new Backend.Profile();
            
            

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // have to do like this to make it async
            GlobalViewModel.UserProfile = await IOClass.LoadProfile();
            await IOClass.LoadActivity();
            await IOClass.LoadHomework();

            RefreshProfileInfo();
            

            SplitViewFrame.Navigate(typeof(LibraryFrame));
        }

        private void RefreshProfileInfo()
        {
            profilenameTB.Text = GlobalViewModel.UserProfile.ProfileName;

            levelTB.Text = Convert.ToString(GlobalViewModel.UserProfile.Level);
            expPB.Maximum = GlobalViewModel.UserProfile.NextLevelExp;
            expPB.Value = GlobalViewModel.UserProfile.CurrentExp;

            cashTB.Text = Convert.ToString(Math.Round(GlobalViewModel.UserProfile.Cash, 2));
            goldTB.Text = Convert.ToString(Math.Round(GlobalViewModel.UserProfile.Gold));
        }

        private void hamburgerBtn_Click(object sender, RoutedEventArgs e)
        {
            reverseSplitPanel();
        }

        private void reverseSplitPanel()
        {
            menuSP.IsPaneOpen = !menuSP.IsPaneOpen;
        }

        private void closeSplitPanel()
        {
            menuSP.IsPaneOpen = false;
        }
                
        private void splitpanelLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            closeSplitPanel();

            if (splitpanelLV.SelectedIndex == 0)
            {
                searchboxSP.Visibility = Visibility.Visible;
            }
            else
            {
                searchboxSP.Visibility = Visibility.Collapsed;
            }

            try
            {
                switch (splitpanelLV.SelectedIndex)
                {
                    case 0:
                        titleTB.Text = "Library";
                        SplitViewFrame.Navigate(typeof(LibraryFrame));
                        break;

                    case 1:
                        titleTB.Text = "Tasks";
                        SplitViewFrame.Navigate(typeof(HomeworkFrame));
                        break;
                    case 2:
                        titleTB.Text = "Equipment";
                        SplitViewFrame.Navigate(typeof(EquipmentFrame));
                        break;
                    case 3:
                        titleTB.Text = "Upgrades";
                        SplitViewFrame.Navigate(typeof(UpgradeFrame));
                        break;
                    case 4:
                        titleTB.Text = "Inventory";
                        SplitViewFrame.Navigate(typeof(InventoryFrame));
                        break;
                    case 5:
                        titleTB.Text = "Store";
                        SplitViewFrame.Navigate(typeof(StoreFrame));
                        break;
                    case 6:
                        titleTB.Text = "Profile";
                        SplitViewFrame.Navigate(typeof(ProfileFrame));
                        break;
                    case 7:                       
                        titleTB.Text = "Activity";
                        SplitViewFrame.Navigate(typeof(ActivityFrame));
                        break;
                }
            }
            catch { } //prevent same page switch to same page
            
        }
        
        private void SplitViewFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (GlobalViewModel.UserProfile != null) RefreshProfileInfo();

            if ( e.SourcePageType!=typeof(LibraryFrame ))
            {
                searchboxSP.Visibility = Visibility.Collapsed;
            }
        }
    }
}
