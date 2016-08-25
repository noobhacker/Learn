using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Learn.Models;

namespace Learn
{
    public static class WebAPI
    {
        public static async Task<List<Book>> GetBooks()      
           => await getFromServerAsync<List<Book>>("Books");
                
        /// <returns>Returns idOnClient</returns>
        public static async Task<int> DownloadBookById(int idOnServer)
        {
            var db = new DatabaseContext();
            var book = await getFromServerAsync<Book>("Books/" + idOnServer);
            var questions = await getFromServerAsync<List<Question>>("GetQuestions/" + idOnServer);

            var result = db.Books.Add(book);
            foreach (var question in questions)
                db.Questions.Add(question);

            await db.SaveChangesAsync();
            return result.Entity.Id;
        }

        public static async Task UploadBook(int bookId)
        {
            var db = new DatabaseContext();
            var book = db.Books.First(x => x.Id == bookId);
            var questions = db.Questions.Where(x => x.BookId == bookId);

            await postToServerAsync<Book>(book, "Books");
            foreach (var question in questions)
                await postToServerAsync<Question>(question, "Questions");
        }

        private static string httpEndpoint = "http://thelearningapp.azurewebsites.net/api/";

        #region HttpCalls
        private static async Task<T> postToServerAsync<T>(object content, string controllerName)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(content);
            var HttpContent = new HttpStringContent(json);
            HttpContent.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/json");

            var url = $"{httpEndpoint}/{controllerName}";
            var httpResponse = await client.PostAsync(new Uri(url), HttpContent);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<T>(responseString);
            return response;
        }

        private static async Task<T> getFromServerAsync<T>(string controllerName)
        {
            var client = new HttpClient();
            var url = $"{httpEndpoint}/{controllerName}";
            var responseString = await client.GetStringAsync(new Uri(url));

            var response = JsonConvert.DeserializeObject<T>(responseString);
            return response;
        }
        #endregion
    }
}
