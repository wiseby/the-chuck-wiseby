using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public Task<ChuckJoke> GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
