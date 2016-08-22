using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Items
{
    class ScrollItem
    {

        public enum ScrollDuration { OneRound, Hour, Permanant}
        public enum ScrollModifyProperty { Points, EXP, Gold}
        

        public ScrollDuration Duration { get; set; }
        public ScrollModifyProperty ModifyProperty { get; set; }
        public int DurationInHour { get; set; }

        public float ModifyValue { get; set; }
                

    }
}
