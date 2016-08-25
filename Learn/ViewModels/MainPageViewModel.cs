using Learn.Items;
using Learn.Models;
using Learn.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<HamburgerMenuItem> HamburgerMenuItems { get; set; }

        private string[] icons = { "", "", "", "", "", "" }; //http://modernicons.io/segoe-mdl2/cheatsheet/
        private string[] texts = { "Online", "Library", "Tasks", "Upgrades", "Profile", "Activity" };

        private Type[] frames = { typeof(OnlinePage), typeof(LibraryPage), typeof(HomeworkPage), typeof(UpgradePage),
                                  typeof(ProfilePage), typeof(ActivityPage) }; // insert page here typeof(Page)

        private string title;
        private string profileName;
        private int level;
        private int exp;
        private int levelUpExp;
        private string skin;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

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

        public string Skin
        {
            get
            {
                return skin;
            }

            set
            {
                skin = value;
                OnPropertyChanged();
            }
        }     

        public async void CheckIfLevelUp()
        {
            var gotLevelUp = false;
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
                gotLevelUp = true;
            }

            if(gotLevelUp)
            {
                var db = new DatabaseContext();
                db.Users.First().Level = Level;
                db.Users.First().CurrentExp = Exp;
                db.Users.First().NextLevelExp = LevelUpExp;
                await db.SaveChangesAsync();
            }
        }

        public MainPageViewModel()
        {
            HamburgerMenuItems = new ObservableCollection<HamburgerMenuItem>();
            for (int i = 0; i < icons.Length; i++)
            {
                HamburgerMenuItems.Add(new HamburgerMenuItem()
                {
                    Icon = icons[i],
                    Text = texts[i],
                    TargetFrame = frames[i]
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
