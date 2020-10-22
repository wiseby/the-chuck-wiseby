using Newtonsoft.Json;
using System.Collections.Generic;

namespace the_chuck_wiseby.Models
{
    public class SearchResponse
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("result")]
        public IEnumerable<ChuckJoke> Jokes { get; set; }
    }
}
