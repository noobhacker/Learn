using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public string HomeworkName { get; set; }
        public DateTime DueDate { get; set; }
        public int Points { get; set; }
    }
}
