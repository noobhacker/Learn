using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Learn.Items
{
    class SkinItem
    {
        public string SkinName { get; set; }
        public int Price { get; set; }
        public SolidColorBrush BaseColor { get; set; }

        //public SolidColorBrush Color2 { get; set; }
        //public SolidColorBrush Color3 { get; set; }
        public SolidColorBrush FontColor { get; set; } // future implementation
        //public bool IsPurchased { get; set; }
        // for loop, not purchased show is shop, 
        // is purchased show in inventory
        public bool IsEquiped { get; set; }
    }
}
