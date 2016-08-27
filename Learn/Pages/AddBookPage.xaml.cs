using Learn.Items;
using Learn.Models;
using Learn.ViewModels;
using System;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Learn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBookPage : Page
    {
        AddBookViewModel vm = new AddBookViewModel();
        private int bookId = -1;
        public AddBookPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += (sender, e) =>
            {
                Frame.Navigate(typeof(LibraryPage));
            };
        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null)
            {
                bookId = Convert.ToInt32(e.Parameter);
                var db = new DatabaseContext();
                vm.BookTitle = db.Books.FirstOrDefault(x => x.Id == bookId).Title;
                foreach (var question in db.Questions.Where(x => x.BookId == bookId))
                {
                    vm.QuestionsList.Add(new QuestionItem()
                    {
                        AnswerString = question.AnswerString,
                        QuestionImageId = question.QuestionImageId,
                        QuestionString = question.QuestionString,
                        QuestionImagePath = question.QuestionString == "" ?
                             ApplicationData.Current.LocalFolder.Path + "\\Images\\" + question.QuestionImageId :
                             "",
                        QuestionImageVisibility = question.QuestionString == "" ? Visibility.Visible : Visibility.Collapsed
                    });
                }
            }
        }

        private void easyModeAddBtn_Click(object sender, RoutedEventArgs e)
        {           
            string str = vm.TextQuestions;
            if (str != "")
            {
                vm.TextQuestions = "";

                string[] lines = str.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

                //loop through each line
                for (int i = 0; i < lines.Length; i++)
                {

                    //// HAVE TO FIX THIS STUPID PROBLEM SINCE FORM 2
                    //if (lines[i].Contains("\r")) lines[i].Replace("\r", "");

                    // to make sure isnt empty "enter" and shit value
                    if (lines[i].Contains(";"))
                    {
                        //extract data from each ;
                        string[] strs = lines[i].Split(';');

                        //add to local binding which will save to local file later

                        vm.QuestionsList.Add(new QuestionItem()
                        {
                            QuestionString = strs[0],
                            QuestionImageVisibility = Visibility.Collapsed,
                            AnswerString = strs[1]
                        });
                    }
                }
            }
        }
        
        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            var db = new DatabaseContext();
            var result = db.Books.Add(new Book()
            {
                Title = vm.BookTitle
            });

            await db.SaveChangesAsync();

            foreach (var item in vm.QuestionsList)
            {
                db.Questions.Add(new Question()
                {
                    BookId = result.Entity.Id,
                    QuestionString = item.QuestionString,
                    AnswerString = item.AnswerString,
                    QuestionImageId = item.QuestionImageId,
                });
            }

            if (bookId != -1)
            {
                db.Remove(db.Books.FirstOrDefault(x => x.Id == bookId));
                foreach (var question in db.Questions.Where(x => x.BookId == bookId))
                    db.Remove(question);
            }

                await db.SaveChangesAsync();

            ClearEverything();
            
            Frame.Navigate(typeof(LibraryPage));
        }

        private void ClearEverything()
        {
            ClearAddImage();
            ClearAddString();
            vm.BookTitle = "";
        }

        private void ClearAddImage()
        {
            vm.ImagePath = "";
            vm.ImageAnswer = "";
            previewImage.Source = new BitmapImage();
        }

        private void ClearAddString()
        {
            vm.TextQuestions = "";
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


                vm.ImagePath = file.Path;
                
                    previewImage.Source = new BitmapImage(new Uri(folder.Path + "\\" + imageid));
                currentimageid = imageid;
                
            }
            
        }

        int currentimageid; // used to pass the id calculated from browse (copy image to local) to binding
        private void guidedModeAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(vm.ImagePath != "" && vm.ImageAnswer != "")
            {
                vm.QuestionsList.Add(new QuestionItem()
                {
                    QuestionImagePath = ApplicationData.Current.LocalFolder.Path + "\\Images\\" + currentimageid,
                    QuestionImageId = currentimageid,
                    QuestionImageVisibility = Visibility.Visible,
                    AnswerString = vm.ImageAnswer

                });
                ClearAddImage();
            }
        }
    }
}
