﻿using Learn.ViewModels;
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
        public static MainPageViewModel vm = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();         
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SplitViewFrame.Navigate(typeof(LibraryPage));
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
                        titleTB.Text = "Internet";

                        break;
                    case 1:
                        titleTB.Text = "Library";
                        SplitViewFrame.Navigate(typeof(LibraryPage));
                        break;

                    case 2:
                        titleTB.Text = "Tasks";
                        SplitViewFrame.Navigate(typeof(HomeworkPage));
                        break;
                    case 3:
                        titleTB.Text = "Equipment";
                        SplitViewFrame.Navigate(typeof(EquipmentPage));
                        break;
                    case 4:
                        titleTB.Text = "Upgrades";
                        SplitViewFrame.Navigate(typeof(UpgradePage));
                        break;
                    case 5:
                        titleTB.Text = "Profile";
                        SplitViewFrame.Navigate(typeof(ProfilePage));
                        break;
                    case 6:                       
                        titleTB.Text = "Activity";
                        SplitViewFrame.Navigate(typeof(ActivityPage));
                        break;
                }
            }
            catch { } //prevent same page switch to same page
            
        }
    }
}
