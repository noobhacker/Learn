using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Helpers
{
    public static class BonusHelper
    {
        public static int GetComboBonusByLevel(int level)
            => level * 1500;

        public static int GetGoldBonusByLevel(int level)
            => level * 2;

        public static int GetWarningUpgradeCostByLevel(int level)
         => Convert.ToInt32(Math.Pow((level + 1), 2) * 10000);

        public static int GetComboUpgradeCostByLevel(int level)
           => (level + 1) * 15000;

        public static int GetGoldUpgradeCostByLevel(int level)
            => (level + 1) * 25000;
    }
}
