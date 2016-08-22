using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Learn.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string profileName;
        private string level;
        private long exp;
        private long levelUpExp;
        private SolidColorBrush skinColor;

        public string ProfileName
        {
            get
            {
                return profileName;
            }

            set
            {
                profileName = value;
                OnPropertyChanged();
            }
        }

        public string Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
                OnPropertyChanged();
            }
        }

        public long Exp
        {
            get
            {
                return exp;
            }

            set
            {
                exp = value;
                OnPropertyChanged();
            }
        }

        public long LevelUpExp
        {
            get
            {
                return levelUpExp;
            }

            set
            {
                levelUpExp = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush SkinColor
        {
            get
            {
                return skinColor;
            }

            set
            {
                skinColor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
