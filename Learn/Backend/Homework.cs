using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Backend
{
    class Homework
    {
        // homework category add later, make program work first

        public string HomeworkName { get; set; }

        // no Date class, so use DateTime with Time 00:00:00
        public DateTime DueDate { get; set; }

        public int Points { get; set; }


    }
}
