using Learn.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBookFrame : Page
    {
        public AddBookFrame()
        {
            this.InitializeComponent();
            questionListLV.ItemsSource = QuestionsList;
        }

        ObservableCollection<QuestionItem> QuestionsList = new ObservableCollection<QuestionItem>();

        private void easyModeAddBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string str = easyModeTB.Text;
            easyModeTB.Text = "";

            string[] lines = str.Split(new string[] { Environment.NewLine },StringSplitOptions.None);
           

            //loop through each line
            for(int i = 0;i<lines.Length;i++)
            {

                //// HAVE TO FIX THIS STUPID PROBLEM SINCE FORM 2
                //if (lines[i].Contains("\r")) lines[i].Replace("\r", "");

                // to make sure isnt empty "enter" and shit value
                if (lines[i] != "" && lines[i].Contains(";"))
                {
                    //extract data from each ;
                    string[] strs = lines[i].Split(';');

                    //add to local binding which will save to local file later

                    //QuestionsList.Add(new Backend.Question()
                    //{
                    //    QuestionString = strs[0],
                    //    QuestionImageVisibility = Visibility.Collapsed,
                    //    AnswerString = new string[] { strs[1] }
                    //});
                }
            }
        }
        
        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //GlobalViewModel.Books.Add( new Backend.Book()
            //{
            //    BookTitle = booktitleTB.Text,
            //    Category = categoryTB.Text,
            //    Description = descriptionTB.Text,
            //    QuestionType  = Backend.Book.QuestionTypes.StringQuestion,
            //    QuestionList = QuestionsList
            //});
            //await IOClass.SaveBooks();
            ClearEverything();

            this.Frame.Navigate(typeof(LibraryFrame));
        }

        private void ClearEverything()
        {
            ClearAddImage();
            ClearAddString();
            booktitleTB.Text = "";
            categoryTB.Text = "";
            descriptionTB.Text = "";
        }

        private void ClearAddImage()
        {
            questionImageTB.Text = "";
            answerTB.Text = "";
            previewImage.Source = new BitmapImage();
        }

        private void ClearAddString()
        {

        }

        private async void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fop = new FileOpenPicker();

            // add dialog filter here
            fop.FileTypeFilter.Add("*");

            var file = await fop.PickSingleFileAsync();

            StorageFolder folder;
            try
            {
                folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");
            }
            catch
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images");
                folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");
            }
                   
            if (file != null)
            {
                int imageid = 1;
                for (int i = 1; i < int.MaxValue; i++) //0 when null
                {
                    try
                    {
                        await file.CopyAsync(folder,i.ToString());
                        imageid = i;
                        break;
                    }
                    catch { } //catch exception means file id exists, go next id
                }


                questionImageTB.Text = file.Path;
                
                    previewImage.Source = new BitmapImage(new Uri(folder.Path + "\\" + imageid));
                currentimageid = imageid;
                
            }
            
        }

        int currentimageid; // used to pass the id calculated from browse (copy image to local) to binding
        private void guidedModeAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(questionImageTB.Text != "" && answerTB.Text != "")
            {

                // qt = Backend.Book.QuestionTypes.ImageQuestion;
                
                string[] answers;
                if(answerTB.Text.Contains(";"))
                {
                    answers = answerTB.Text.Split(';');
                }
                else
                {
                    answers = new string[] { answerTB.Text };
                }

                //QuestionsList.Add(new Backend.Question()
                //{
                //    // QuestionObject = previewImage.Source,
                //    QuestionImagePath = ApplicationData.Current.LocalFolder.Path + "\\Images\\" + currentimageid,
                //    QuestionImageID = currentimageid,
                //    QuestionImageVisibility = Visibility.Visible,
                //    AnswerString = answers
                    
                //});
                ClearAddImage();
            }
        }
    }
}
