using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Learn.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        private int level;
        private int exp;
        private int levelUpExp;
        private int gold;
        private PointCollection trianglePoints;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public int Level
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

        public int Exp
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

        public int LevelUpExp
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

        public int Gold
        {
            get
            {
                return gold;
            }

            set
            {
                gold = value;
                OnPropertyChanged();
            }
        }

        public PointCollection TrianglePoints
        {
            get
            {
                return trianglePoints;
            }

            set
            {
                trianglePoints = value;
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
