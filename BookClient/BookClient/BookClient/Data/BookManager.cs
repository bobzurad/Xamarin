using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookClient.Data
{
    public class BookManager
    {
		const string Url = "http://xam150.azurewebsites.net/api/books/";
		private string authorizationKey;

        public async Task<IEnumerable<Book>> GetAll()
        {
			var client = await GetClient();
			var json = await client.GetStringAsync(Url);

			return JsonConvert.DeserializeObject<IEnumerable<Book>>(json);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
			var book = new Book {
				ISBN = String.Empty,
				Title = title,
				Authors = new List<string>() { author },
				Genre = genre,
				PublishDate = DateTime.Now
			};

			var client = await GetClient();
			var json = JsonConvert.SerializeObject(book);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(Url, content);

			json = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<Book>(json);
        }

        public async Task Update(Book book)
        {
			var client = await GetClient();
			var json = JsonConvert.SerializeObject(book);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			await client.PutAsync(Url + book.ISBN, content);
		}

        public async Task Delete(string isbn)
        {
			var client = await GetClient();

			await client.DeleteAsync(Url + isbn);
        }

		private async Task<HttpClient> GetClient()
		{
			var client = new HttpClient();

			if (string.IsNullOrEmpty(authorizationKey)) {
				var token = await client.GetStringAsync(Url + "login");
				authorizationKey = JsonConvert.DeserializeObject<string>(token);
			}

			client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			return client;
		}
    }
}

