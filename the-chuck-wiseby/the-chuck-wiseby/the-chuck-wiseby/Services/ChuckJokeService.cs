using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using the_chuck_wiseby.Models;

namespace the_chuck_wiseby.Services
{
    public class ChuckJokeService : IHttpService<ChuckJoke>
    {
        private HttpClient client;
        private readonly string baseUrl = "https://api.chucknorris.io/jokes/";
        public ChuckJokeService()
        {
            // This should be a dependency injected in the constructor!!!
            // Leaving this to get project rolling!
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var response = await this.client.GetAsync("categories");

            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var categories = JsonConvert.DeserializeObject<List<string>>(jsonResponse);

                return categories;
            }

            else
            {
                throw new NullReferenceException($"sorry api returned status: {response.StatusCode}");
            }
        }

        public Task<ChuckJoke> GetByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<ChuckJoke> GetBySearch(string searchTerm)
        {
            var builder = new UriBuilder(baseUrl);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["search"] = searchTerm;
            builder.Query = query.ToString();
            string url = builder.ToString();

            throw new NotImplementedException();
        }

        public async Task<ChuckJoke> GetRandom()
        {
            var response = await this.client.GetAsync("random");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var joke = JsonConvert.DeserializeObject<ChuckJoke>(jsonResponse);

                return joke;
            }

            else
            {
                throw new NullReferenceException($"sorry api returned status: {response.StatusCode}");
            }
        }
    }
}
