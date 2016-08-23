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
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //    profilenameTB.Text = GlobalViewModel.UserProfile.ProfileName;
            //    levelTB.Text = Convert.ToString(GlobalViewModel.UserProfile.Level);
            //    currentexpTB.Text = Convert.ToString(GlobalViewModel.UserProfile.CurrentExp);
            //    maxexpTB.Text = Convert.ToString(GlobalViewModel.UserProfile.NextLevelExp);

            //    expPB.Maximum = GlobalViewModel.UserProfile.NextLevelExp;
            //    expPB.Value  = GlobalViewModel.UserProfile.CurrentExp;

            //    cashTB.Text = Convert.ToString(Math.Round(GlobalViewModel.UserProfile.Cash,2));
            //    goldTB.Text = Convert.ToString(Math.Round(GlobalViewModel.UserProfile.Gold,2));

            //    drawTriangle();
        }

    //private void drawTriangle()
    //{
    //    int[] exps = { GlobalViewModel.UserProfile.ReadingEXP * 100,
    //    GlobalViewModel.UserProfile.TestEXP,
    //    GlobalViewModel.UserProfile.HomeworkEXP};

    //    int maxvalue = exps.Max();

    //    // if equals zero means first time no need to draw the triangle
    //    if (maxvalue != 0)
    //    {
    //        for (int i = 0; i < exps.Length; i++)
    //        {
    //            exps[i] = exps[i] / maxvalue * 100; //this will turn them into 0 to 100
    //        }

    //        var positions = new PointCollection();

    //        positions.Add(new Point(
    //            100, 90 - (exps[0] / 100 * 90)));

    //        positions.Add(new Point(
    //            (100 - exps[1]), ((exps[1] / 100 * 60) + 90)));

    //        positions.Add(new Point(
    //            (100 + exps[2]), ((exps[2] / 100 * 60) + 90)));

    //        statisticstriangle.Points = positions;
    //    }

}

}
