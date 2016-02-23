﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Backend
{
    class Profile : INotifyPropertyChanged
    {

        public string ProfileName { get; set; }

        public ObservableCollection<Skin> PurchasedSkin { get; set;}
        public ObservableCollection<Voice> PurchasedVoice { get; set; }
        // these ONLY USED TO DO GRAPH
        // dont mix with level system, very very confuse

        public int ReadingEXP { get; set; }
        public int TestEXP { get; set; }
        public int HomeworkEXP { get; set; }

        public double Cash { get; set; }
        public double Gold { get; set; }

        public double PermanantEXPMultiplier { get; set; }
        public double PermanantGoldMultiplier { get; set; }

        // this two variables are mean at current level
        // the above mean total
        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int NextLevelExp { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // call each time when exp added
        public void CheckIfLevelUp()
        {
            
            while(CurrentExp>NextLevelExp) // so up multilevels at once will work
            {

                CurrentExp -= NextLevelExp;

                // minus first before nextlevelexp got adjustment

                if (Level <= 23)
                {
                    NextLevelExp = Convert.ToInt16(NextLevelExp * (1.3 - Convert.ToDouble(Level) / 100));
                }
                else
                {
                    NextLevelExp = Convert.ToInt16(NextLevelExp* 1.07);
                }

                //level up after multiplications
                Level++;
                
            }

        }

    }
}
