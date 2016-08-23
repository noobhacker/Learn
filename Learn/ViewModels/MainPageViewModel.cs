using Learn.Models;
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
        private int level;
        private int exp;
        private int levelUpExp;
        private string skinColor;

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

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;

                var db = new DatabaseContext();
                db.Users.First().Level = Level;
                db.SaveChangesAsync();

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
                CheckIfLevelUp();
                
                var db = new DatabaseContext();
                db.Users.First().CurrentExp = Exp;
                db.SaveChangesAsync();

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
                
                var db = new DatabaseContext();
                db.Users.First().NextLevelExp = LevelUpExp;
                db.SaveChangesAsync();

                OnPropertyChanged();
            }
        }

        public string SkinColor
        {
            get
            {
                return skinColor;
            }

            set
            {
                skinColor = value;

                var db = new DatabaseContext();
                db.Users.First().SkinColor = SkinColor;
                db.SaveChangesAsync();

                OnPropertyChanged();
            }
        }

        public void CheckIfLevelUp()
        {
            while (Exp > LevelUpExp) // so up multilevels at once will work
            {
                Exp -= LevelUpExp;

                // minus first before nextlevelexp got adjustment

                if (Level <= 23)
                    LevelUpExp = Convert.ToInt16(LevelUpExp * (1.3 - Convert.ToDouble(Level) / 100));
                else
                    LevelUpExp = Convert.ToInt16(LevelUpExp * 1.07);

                //level up after multiplications
                Level++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
