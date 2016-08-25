using Learn.Helpers;
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
using static Learn.Helpers.BonusHelper;

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

            var warningLevel = user.WarningLevel;
            var comboLevel = user.ComboMultiplierLevel;
            var goldLevel = user.GoldMultiplierLevel;

            vm.Upgrades.Add(new UpgradeItem()
            {
                Name = "Warning",
                Level = warningLevel,
                Cost = GetWarningUpgradeCostByLevel(warningLevel),
                Description = $"{warningLevel + 1} times warning before considered as wrong."
            });

            vm.Upgrades.Add(new UpgradeItem()
            {
                Name = "Combo Multiplier",
                Level = comboLevel,
                Cost = GetComboUpgradeCostByLevel(comboLevel),
                Description = $"Adds {GetComboBonusByLevel(comboLevel + 1)} points when full combo."
            });

            vm.Upgrades.Add(new UpgradeItem()
            {
                Name = "Gold Multiplier",
                Level = goldLevel,
                Cost = GetGoldUpgradeCostByLevel(goldLevel),
                Description = $"Increases gold gained by {GetComboBonusByLevel(goldLevel + 1)}%"
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadSkins();
            loadUpgrades();

        }

        private async void skinsGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as GridView).SelectedIndex;
            var selectedSkin = vm.Skins[index];

            var db = new DatabaseContext();
            if (selectedSkin.Price != "")
            {
                if (await DialogHelper.ShowYesNoDialogAsync("Do you want to purchase this skin?") == 0)
                {
                    var price = db.Skins.First(x => x.Name == selectedSkin.Name).Price;
                    if (db.Users.First().Gold > price)
                    {
                        db.Users.First().Gold -= price;
                        db.Skins.First(x => x.Name == selectedSkin.Name).Owned = true;
                        vm.Skins[index].Price = "";
                    }
                    else
                    {
                        await DialogHelper.ShowDialogAsync("You don't have enough gold to purchase this skin");
                    }
                }
                else
                {
                    return;
                }
            }
        
            MainPage.vm.Skin = selectedSkin.Color;
            db.Users.First().SkinColor = selectedSkin.Color;

            await db.SaveChangesAsync();
        }

        private async void upgradesGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as GridView).SelectedIndex;
                var db = new DatabaseContext();
                if (db.Users.First().Gold > vm.Upgrades[index].Cost)
                {
                    switch (index)
                    {
                        case 0:
                            db.Users.First().WarningLevel += 1;
                            break;
                        case 1:
                            db.Users.First().ComboMultiplierLevel += 1;
                            break;
                        case 2:
                            db.Users.First().GoldMultiplierLevel += 1;
                            break;
                    }
                    db.Users.First().Gold -= vm.Upgrades[index].Cost;

                    await db.SaveChangesAsync();

                Frame.Navigate(typeof(UpgradePage));
                }
                else
                {
                    await DialogHelper.ShowDialogAsync("You don't have enough gold to purchase this upgrade");
                }           
        }
    }
}
