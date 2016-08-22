using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeworkFrame : Page
    {
        public HomeworkFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var listviewitems = new List<HomeworkListviewItem>();
            listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Final Year Project", DueDate = "12 May 2016", Points = 13500 });
            listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Calculus 5 questions", DueDate = "12 May 2017", Points = 200 });
            listviewitems.Add(new HomeworkListviewItem { HomeworkName = "Computer Architecture tutorial 5", DueDate = "12 May 2018", Points = 500 });

            homeworksLV.ItemsSource = listviewitems;
            
        }

    }

    public class HomeworkListviewItem
    {
        public string HomeworkName { get; set; }
        public string DueDate { get; set; }  //use string so cleaner inside xaml binding
        public int Points { get; set; }
    }
}
