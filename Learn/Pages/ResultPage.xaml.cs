using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class ResultFrame : Page
    {

        DispatcherTimer animationDT = new DispatcherTimer();

        public ResultFrame()
        {
            this.InitializeComponent();

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
            {
                animatedanswerspeed -= 0.1;
            }
            else if(animatedanswerspeed - tempanswerspeed > 0.03)
            {
                animatedanswerspeed -= 0.01;
            }
            else if(animatedanswerspeed - tempanswerspeed > 0)
            {
                animatedanswerspeed -= 0.001;
            }


            //if(tempcombo - animatedcombo  > 100)
            //{
            //    animatedcombo += 50;
            //}
            //else if ( tempcombo - animatedcombo > 10)
            //{
            //    animatedcombo += 5;
            //}
            //else 

            if (tempcombo - animatedcombo > 0)
            {
                animatedcombo += 1;
            }

            //if(temperror-animatederror > 100)
            //{
            //    animatederror += 50;
            //}
            //else if (temperror - animatederror > 10)
            //{
            //    animatederror += 5;
            //}
            //else 
            if (temperror - animatederror > 0)
            {
                animatederror += 1;
            }

            if (temppoints - animatedpoints > 1500)
            {
                animatedpoints += 1000;
            }
            else if (temppoints - animatedpoints > 150)
            {
                animatedpoints += 100;
            }
            else if (temppoints - animatedpoints > 5)
            {
                animatedpoints += 10;
            }
            else if (temppoints - animatedpoints >0)
            {
                animatedpoints += 1;
            }            

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
                cashgainedTB.Text = Convert.ToString(Math.Round(Convert.ToDouble(temppoints) / 10000, 2));
                goldgainedTB.Text = Convert.ToString(Math.Round(Convert.ToDouble(temppoints) / 100));

                //calculate grade here
                int pointsperquestion = temppoints / report.ResultList.Count;
                string strgrade = "";
                string[] grades = { "C", "C+", "B", "B+", "A", "A+","S","S+"};
                int[] scores = { 50, 75, 100, 125, 250, 500, 1000, 2000 };

                for(int i = 0;i < grades.Length;i++)
                {
                    if (pointsperquestion > scores[i])
                    {
                        strgrade = grades[i];
                       
                      
                    }
                    
                }
                gradeTB.Text = strgrade;

                pointsrewardedGrid.Visibility = Visibility.Visible;

                animationDT.Stop();
            }
        }

        Backend.FullResult report = new Backend.FullResult();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            report = (Backend.FullResult)e.Parameter;
            
            resultGV.ItemsSource = report.ResultList;

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
            {
                points = Convert.ToInt32(report.ResultList.Count * 100 *
                    Math.Pow(2 - dblAverageSec,5));
            }
            else if (dblAverageSec < 2)
            {
                points = report.ResultList.Count * 100;
            }
            else
            {
                points = report.ResultList.Count * 50;
            }
            points = points + (tempcombo * 100) - (interror * 1000);
            temppoints = points;

            // end of calculating points

            // record into file
            GlobalViewModel.UserProfile.Cash += Convert.ToDouble(temppoints) / 10000;
            GlobalViewModel.UserProfile.Gold += temppoints / 100;

            GlobalViewModel.UserProfile.CurrentExp += Convert.ToInt32(((temppoints / 100)* (1+
                Convert.ToDouble(GlobalViewModel.UserProfile.Level)/100)));
            // 1 level + 0.01%, higher level earns more exp makes sense

            // dont forget this!!!
            GlobalViewModel.UserProfile.TestEXP += temppoints / 100;

            GlobalViewModel.UserProfile.CheckIfLevelUp();
            await IOClass.SaveProfile();

            GlobalViewModel.UserActivity.Add(new Backend.Activity()
            {
                ActionDate = DateTime.Now,
                ActionDescription = "Test (" + report.ResultList.Count + " questions)",
                ActionName = "Test",
                ActionPoints = temppoints.ToString()
            });
            await IOClass.SaveActivity();

            await Task.Delay(500);
            animationDT.Start();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LibraryFrame));        }
    }
}
