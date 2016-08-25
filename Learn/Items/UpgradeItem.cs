using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Items
{
    public class UpgradeItem : INotifyPropertyChanged
    {
        private string name;
        private int level;
        private string description;
        private int cost;

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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                this.description = value;
                OnPropertyChanged();
            }
        }

        public int Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
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
