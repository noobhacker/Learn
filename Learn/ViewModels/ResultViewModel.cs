using Learn.Items;
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
    public class ResultViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ResultItem> Results { get; set; }

        private int maxCombo;
        private int comboBonus;
        private int goldBonus;

        public int MaxCombo
        {
            get
            {
                return maxCombo;
            }

            set
            {
                maxCombo = value;
                OnPropertyChanged();
            }
        }

        public int ComboBonus
        {
            get
            {
                return comboBonus;
            }

            set
            {
                comboBonus = value;
                OnPropertyChanged();
            }
        }

        public int GoldBonus
        {
            get
            {
                return goldBonus;
            }

            set
            {
                goldBonus = value;
                OnPropertyChanged();
            }
        }

        public ResultViewModel()
        {
            Results = new ObservableCollection<ResultItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
