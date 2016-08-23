using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string QuestionString { get; set; }
        public int QuestionImageId { get; set; }
        public string AnswerString { get; set; }
    }
}
