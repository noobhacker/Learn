//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.Storage;
//using Newtonsoft.Json;
//using System.Collections.ObjectModel;
//using System.IO;
//using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Media.Imaging;
//using Windows.Graphics.Imaging;
//using Windows.UI.Xaml.Controls;
//using Windows.Storage.Streams;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.UI.Popups;

//namespace Learn
//{
//    class IOClass
//    {
        
//        public static async Task WriteTextToLocalStorageAsync(string filename, string contents)
//        {

//            StorageFolder folder = ApplicationData.Current.LocalFolder;
//            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            
//            await FileIO.WriteTextAsync(file, contents);
//        }

//        public static async Task<string> ReadTextFromLocalStorageAsync(string filename)
//        {

//            StorageFolder folder = ApplicationData.Current.LocalFolder;
//            StorageFile file = await folder.GetFileAsync(filename);

//            string result = await FileIO.ReadTextAsync(file);

//            return result;

//        }

//        public static async Task<bool> SaveBooks()
//        {
           
//            for(int i = 0; i < GlobalViewModel.Books.Count;i++)
//            {
//                for(int j = 0; j < GlobalViewModel.StaticBooks.Count;j++)
//                {
//                    if(GlobalViewModel.Books[i] == GlobalViewModel.StaticBooks[j])
//                    {
//                        GlobalViewModel.Books.RemoveAt(i);
//                    }
//                }
//            }

//            await WriteTextToLocalStorageAsync("Books", JsonConvert.SerializeObject(GlobalViewModel.Books));

//            await LoadStaticBooks();

//            return true;
//        }

//        public static async Task<bool> LoadBooks() //bool to let await / indicate job is done
//        {

           

//            try
//            {
//                GlobalViewModel.Books = JsonConvert.DeserializeObject<ObservableCollection<Backend.Book>>
//                    (await ReadTextFromLocalStorageAsync("Books"));
//            }
//            catch
//            {
//                GlobalViewModel.Books = new ObservableCollection<Backend.Book>();
//                await SaveBooks();
//            }

          

//            return true;
//        }
              
//        public static async Task<bool> LoadStaticBooks()
//        {
//            try
//            {
//                loadFromInternalFile("japanese");
//                loadFromInternalFile("chinese");

//            }
//            catch { await new MessageDialog("Internal books file corrupted").ShowAsync(); }
//            return true;
//        }

//        private static void loadFromInternalFile(string filename)
//        {
//            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
//            var text = loader.GetString(filename);

//            GlobalViewModel.StaticBooks = new ObservableCollection<Backend.Book>();

//            ObservableCollection<Backend.Book> b = JsonConvert.DeserializeObject<ObservableCollection<Backend.Book>>
//                (text);

//            foreach (var i in b)
//            {
//                GlobalViewModel.Books.Add(i);
//                GlobalViewModel.StaticBooks.Add(i);
//            }
//        }

//        public static async Task<Backend.Profile> LoadProfile()
//        {
//            try
//            {
//                return JsonConvert.DeserializeObject<Backend.Profile>(await ReadTextFromLocalStorageAsync("profile"));
//            }
//            catch
//            {
//                // new user

//                // add user decided name later
//                GlobalViewModel.UserProfile.ProfileName = "Developer";
//                GlobalViewModel.UserProfile.Level = 1;
//                GlobalViewModel.UserProfile.NextLevelExp = 100;

//                await SaveProfile();

//                return JsonConvert.DeserializeObject<Backend.Profile>(await ReadTextFromLocalStorageAsync("profile"));
//            }
            
//        }

//        public static async Task<bool> SaveProfile()
//        {
//            await WriteTextToLocalStorageAsync("profile", JsonConvert.SerializeObject(GlobalViewModel.UserProfile));
//            return true;
//        }

//        // dun need load skin, just for loop check isequiped

//        public static async Task<bool> LoadActivity()
//        {
//            try
//            {
//                GlobalViewModel.UserActivity = JsonConvert.DeserializeObject<ObservableCollection<Backend.Activity>>
//                    (await ReadTextFromLocalStorageAsync("activity"));

//            }
//            catch
//            {
//                // create new one if exception and save
//                GlobalViewModel.UserActivity = new ObservableCollection<Backend.Activity>();
//                await SaveActivity();
//                await LoadActivity();
//            }
//            return true;
//        }

//        public static async Task<bool> SaveActivity()
//        {
//            await WriteTextToLocalStorageAsync("activity", JsonConvert.SerializeObject(GlobalViewModel.UserActivity));
//            return true;
//        }
//        public static async Task<bool> LoadHomework()
//        {
//            try
//            {
//                GlobalViewModel.UserHomework = 
//                    JsonConvert.DeserializeObject<ObservableCollection<Backend.Homework>>
//                    (await ReadTextFromLocalStorageAsync("homework"));

//            }
//            catch
//            {
//                GlobalViewModel.UserHomework = new ObservableCollection<Backend.Homework>();
//                await SaveHomework();
//                await LoadHomework();
//            }
//            return true;
//        }

//        public static async Task<bool> SaveHomework()
//        {
//            await WriteTextToLocalStorageAsync("homework", JsonConvert.SerializeObject(GlobalViewModel.UserHomework));
//            return true;
//        }
//    }
//}
