using Learn.Items;
using Learn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ViewModels
{
    public class UpgradeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UpgradeItem> Upgrades { get; set; }

        public ObservableCollection<SkinItem> Skins { get; set; }

        public List<Skin> SkinList = new List<Skin>();

        public UpgradeViewModel()
        {
            // need this to update available skinlist in database
            initializeSkinList();

            Upgrades = new ObservableCollection<UpgradeItem>();
            Skins = new ObservableCollection<SkinItem>();
        }

        private void initializeSkinList()
        {
            SkinList.Add(new Skin()
            {
                Color = "",
                Name = "System",
                Owned = true,
                Price = 0
            });
            SkinList.Add(new Skin()
            {
                Color = "#28FF99",
                Name = "Summer",
                Owned = false,
                Price = 5000
            });
            SkinList.Add(new Skin()
            {
                Color = "#FF853F",
                Name = "Fall",
                Owned = false,
                Price = 5000
            });
            SkinList.Add(new Skin()
            {
                Color = "#09A8E8",
                Name = "Winter",
                Owned = false,
                Price = 5000
            });
            SkinList.Add(new Skin()
            {
                Color = "#AE3CFF",
                Name = "Purple",
                Owned = false,
                Price = 5000
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
