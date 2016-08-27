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
        public static async Task<List<Book>> GetBooksAsync()
           => await getFromServerAsync<List<Book>>("Books");

        /// <returns>Returns idOnClient</returns>
        public static async Task<int> DownloadBookByIdAsync(int idOnServer)
        {
            var db = new DatabaseContext();
            var book = await getFromServerAsync<Book>("Books/" + idOnServer);
            var questions = await getFromServerAsync<List<Question>>("Questions/" + idOnServer);

            var result = db.Books.Add(new Book()
            {
                Title = book.Title
            });

            await db.SaveChangesAsync();
            var bookId = result.Entity.Id;
            
            foreach (var question in questions)
            {
                db.Questions.Add(new Question()
                {
                    AnswerString = question.AnswerString,
                    QuestionString = question.QuestionString,
                    BookId = bookId
                });
            }

            await db.SaveChangesAsync();
            return bookId;
        }

        public static async Task UploadBookAsync(int bookId)
        {
            var db = new DatabaseContext();
            var book = db.Books.First(x => x.Id == bookId);
            var questions = db.Questions.Where(x => x.BookId == bookId);

            var result = await postToServerAsync<Book>(book, "Books");
            foreach (var question in questions)
            {
                // get book id from server then add questions
                question.BookId = result.Id;
                var questionResult = await postToServerAsync<Question>(question, "Questions");
            }
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
