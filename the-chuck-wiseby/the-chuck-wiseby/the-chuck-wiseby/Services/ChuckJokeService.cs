using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using the_chuck_wiseby.Models;

namespace the_chuck_wiseby.Services
{
    public class ChuckJokeService : IHttpService<ChuckJoke, ChuckMessage>
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

        public async Task<ChuckJoke> GetJoke(ChuckMessage message)
        {
            if (message.Category != null)
            {
                return await GetByCategory(message.Category);
            }
            else
            {
                return await GetRandom();
            }
        }

        public async Task<ChuckJoke> GetByCategory(string category)
        {
            var request = BuildUriWithQuery(
                new Dictionary<string,string>() {{"category", category}},
                "random"
                );
            
            var response = await this.client.GetAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var joke = JsonConvert.DeserializeObject<ChuckJoke>(jsonResponse);

                return joke;
            }

            else { return null; }
        }

        public async Task<SearchResponse> GetBySearch(string searchTerm)
        {
            var request = BuildUriWithQuery(
                new Dictionary<string,string>() {{"query", searchTerm}},
                "search"
                );
            
            var response = await this.client.GetAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var jokes = JsonConvert.DeserializeObject<SearchResponse>(jsonResponse);

                return jokes;
            }

            else
            {
                return new SearchResponse() { Jokes = new List<ChuckJoke>(), Total = 0 };
            }
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

        private Uri BuildUriWithQuery(Dictionary<string, string> parameters, string uriResource)
        {
            var builder = new UriBuilder($"{baseUrl}{uriResource}");
            
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var kvp in parameters)
            {
                query[kvp.Key] = kvp.Value;
            }
            builder.Query = query.ToString();
            return builder.Uri;
        }
    }
}
