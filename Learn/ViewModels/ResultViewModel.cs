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
        // these are not needed since objects are passed through FullResult

        //public ObservableCollection<ResultItem> ResultList = new ObservableCollection<ResultItem>();
        
        //private int maxCombo;

        //public int MaxCombo
        //{
        //    get
        //    {
        //        return maxCombo;
        //    }

        //    set
        //    {
        //        maxCombo = value;
        //        OnPropertyChanged();
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
