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
        private int warningLevel;
        private int warningValue;
        private int warningGold;

        private int comboLevel;
        private int comboValue;
        private int comboGold;

        private int goldLevel;
        private int goldValue;
        private int goldGold;
      
        public int WarningLevel
        {
            get
            {
                return warningLevel;
            }

            set
            {
                warningLevel = value;
                OnPropertyChanged();
            }
        }

        public int WarningValue
        {
            get
            {
                return warningValue;
            }

            set
            {
                warningValue = value;
                OnPropertyChanged();
            }
        }

        public int WarningGold
        {
            get
            {
                return warningGold;
            }

            set
            {
                warningGold = value;
                OnPropertyChanged();
            }
        }

        public int ComboLevel
        {
            get
            {
                return comboLevel;
            }

            set
            {
                comboLevel = value;
                OnPropertyChanged();
            }
        }

        public int ComboValue
        {
            get
            {
                return comboValue;
            }

            set
            {
                comboValue = value;
                OnPropertyChanged();
            }
        }

        public int ComboGold
        {
            get
            {
                return comboGold;
            }

            set
            {
                comboGold = value;
                OnPropertyChanged();
            }
        }

        public int GoldLevel
        {
            get
            {
                return goldLevel;
            }

            set
            {
                goldLevel = value;
                OnPropertyChanged();
            }
        }

        public int GoldValue
        {
            get
            {
                return goldValue;
            }

            set
            {
                goldValue = value;
                OnPropertyChanged();
            }
        }

        public int GoldGold
        {
            get
            {
                return goldGold;
            }

            set
            {
                goldGold = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SkinItem> Skins { get; set; }
        public List<Skin> SkinList = new List<Skin>();

        public UpgradeViewModel()
        {
            // need this to update available skinlist in database
            initializeSkinList();

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
