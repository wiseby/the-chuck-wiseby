using System.Collections.Generic;
using System.Threading.Tasks;

namespace the_chuck_wiseby.Services
{
    public interface IDataStore<T>
    {
        void Initialize(string dbName);
        Task<int> Save(T data);
        Task<int> Delete(T data);
        Task<IEnumerable<T>> Load();
    }
}
