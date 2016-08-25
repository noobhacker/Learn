using Learn.Items;
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

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpgradePage : Page
    {
        UpgradeViewModel vm = new UpgradeViewModel();
        public UpgradePage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private async void loadSkins()
        {
            var db = new DatabaseContext();
            foreach (var skin in vm.SkinList.Where(x => !db.Skins.Any(y => x.Name == y.Name)))
                db.Skins.Add(skin);

            await db.SaveChangesAsync();

            foreach (var skin in db.Skins)
            {
                vm.Skins.Add(new SkinItem()
                {
                    Color = skin.Color,
                    Name = skin.Name,
                    Price = skin.Owned ? "" : skin.Price + " gold",
                    Id = skin.Id
                });
            }
        }

        private void loadUpgrades()
        {
            var db = new DatabaseContext();
            var user = db.Users.First();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadSkins();
            loadUpgrades();

        }

        private void skinsGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as GridView).SelectedIndex;

        }
    }
}
