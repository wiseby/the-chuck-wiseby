using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace the_chuck_wiseby.Services
{
    public interface IHttpService<T>
    {
        Task<IEnumerable<string>> GetCategories();
        Task<T> GetByCategory(string category);
        Task<T> GetRandom();
        Task<T> GetBySearch(string searchTerm);
    }
}
