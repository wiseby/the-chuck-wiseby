using System.Collections.Generic;
using System.Threading.Tasks;
using the_chuck_wiseby.Models;

namespace the_chuck_wiseby.Services
{
    public interface IHttpService<T, U>
    {
        Task<T> GetJoke(U message);
        Task<IEnumerable<string>> GetCategories();
        Task<T> GetByCategory(string category);
        Task<T> GetRandom();
        Task<SearchResponse> GetBySearch(string searchTerm);
    }
}
