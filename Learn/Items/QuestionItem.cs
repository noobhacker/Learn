using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Learn.Items
{
    public class QuestionItem
    {
        public string QuestionString { get; set; }
        
        // this is for binding and display image
        public string QuestionImagePath { get; set; }
        
        // this will provide file name for everytime the local settings folder changes path
        public int QuestionImageID { get; set; }
        public Visibility QuestionImageVisibility { get; set; }
        public string AnswerString { get; set; }

    }

   
}
