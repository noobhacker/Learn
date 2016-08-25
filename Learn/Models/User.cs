using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public int Gold { get; set; }

        public int Level { get; set; }

        public int ReadingEXP { get; set; }
        public int TestEXP { get; set; }
        public int HomeworkEXP { get; set; }
        public int CurrentExp { get; set; }
        public int LevelUpExp { get; set; }

        public int WarningLevel { get; set; }
        public int ComboMultiplierLevel { get; set; }
        public int GoldMultiplierLevel { get; set; }

        public string SkinColor { get; set; }
    }
}
