using Learn.Items;
using Learn.Models;
using Learn.Objects;
using Learn.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        ResultViewModel vm = new ResultViewModel();
        DispatcherTimer animationDT = new DispatcherTimer();

        public ResultPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;

            animationDT.Interval = new TimeSpan(0, 0, 0, 0, 1);
            animationDT.Tick += animationDT_Tick;           
        }

        #region animationvariables

        double tempanswerspeed;
        int tempcombo;
        int temperror;
        int temppoints;

        double animatedanswerspeed = 5;
        int animatedcombo = 0;
        int animatederror = 0;
        int animatedpoints = 0;

        #endregion 

        private void animationDT_Tick(object sender, object e)
        {
            if (animatedanswerspeed - tempanswerspeed > 0.2)
                animatedanswerspeed -= 0.1;
            else if(animatedanswerspeed - tempanswerspeed > 0.03)
                animatedanswerspeed -= 0.01;
            else if(animatedanswerspeed - tempanswerspeed > 0)
                animatedanswerspeed -= 0.001;

            if (tempcombo - animatedcombo > 0)
                animatedcombo += 1;
       
            if (temperror - animatederror > 0)
                animatederror += 1;
            if (temppoints - animatedpoints > 1500)
                animatedpoints += 1000;
            else if (temppoints - animatedpoints > 150)          
                animatedpoints += 100;          
            else if (temppoints - animatedpoints > 5)           
                animatedpoints += 10;            
            else if (temppoints - animatedpoints >0)       
                animatedpoints += 1;                    

            averagesecTB.Text = Convert.ToString(Math.Round(animatedanswerspeed,3));
            maxcomboTB.Text = Convert.ToString(animatedcombo);
            errorTB.Text = Convert.ToString(animatederror);
            pointsTB.Text = Convert.ToString(animatedpoints);

            if(tempanswerspeed >= Math.Round(animatedanswerspeed,3) && 
                tempcombo == animatedcombo &&
                temperror == animatederror &&
                temppoints == animatedpoints)
            {
                // display gained exp gold etc. here
                goldgainedTB.Text = Convert.ToString(Math.Round(Convert.ToDouble(temppoints) / 100));

                //calculate grade here
                int pointsperquestion = temppoints / report.ResultList.Count;
                string strgrade = "";
                string[] grades = { "C", "C+", "B", "B+", "A", "A+","S","S+"};
                int[] scores = { 50, 75, 100, 125, 250, 500, 1000, 2000 };

                for(int i = 0;i < grades.Length;i++)
                    if (pointsperquestion > scores[i])
                        strgrade = grades[i];                     
                    
                gradeTB.Text = strgrade;

                pointsrewardedGrid.Visibility = Visibility.Visible;

                animationDT.Stop();
            }
        }

        FullResult report = new FullResult();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            report = (FullResult)e.Parameter;
            foreach (var item in report.ResultList)
                vm.ResultList.Add(item);
            vm.MaxCombo = report.MaxCombo;
            tempcombo = report.MaxCombo;

            double dblAverageSec = 0;
            int interror = 0;
            for (int i = 0; i < report.ResultList.Count; i++) 
            {
                // it was string because i want to round it without 0.00000001 and bind to UI
                dblAverageSec += Convert.ToDouble(report.ResultList[i].AnswerSpeed);
                interror += report.ResultList[i].ErrorCount;
            }

            dblAverageSec /= report.ResultList.Count;
            tempanswerspeed = Math.Round(dblAverageSec, 3);
            temperror = interror;

            // calculate points
            int points = 0;
            
            if (dblAverageSec < 1)        
                points = Convert.ToInt32(report.ResultList.Count * 100 * Math.Pow(2 - dblAverageSec,5));      
            else if (dblAverageSec < 2)        
                points = report.ResultList.Count * 100;         
            else        
                points = report.ResultList.Count * 50;
        
            points = points + (tempcombo * 100) - (interror * 1000);
            temppoints = points;

            // end of calculating points
            // update database

            var db = new DatabaseContext();
            var user = db.Users.First();

            user.Gold += temppoints / 100;

            var expAmount = Convert.ToInt32(((temppoints / 100) * (1 +
                Convert.ToDouble(MainPage.vm.Level) / 100)));
            user.CurrentExp += expAmount;
            MainPage.vm.Exp += expAmount;
            // 1 level + 0.01%, higher level earns more exp makes sense

            //// dont forget this!!!
            user.TestEXP += temppoints / 100;

            db.Activities.Add(new Activity()
            {
                Date = DateTime.Now,
                Description = "Test (" + report.ResultList.Count + " questions)",
                Name = "Test",
                Points = temppoints
            });
           
            await db.SaveChangesAsync();

            await Task.Delay(500);
            animationDT.Start();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LibraryPage));
        }
    }
}
