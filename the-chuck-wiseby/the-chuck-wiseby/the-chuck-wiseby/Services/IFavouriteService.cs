using System.Collections.Generic;
using System.Threading.Tasks;

namespace the_chuck_wiseby.Services
{
    public interface IFavouriteService<T>
    {
        void Initialize();
        IEnumerable<T> Get();
        void Save(T item);
        void Delete(T item);
        bool IsFavourite(T item);
    }
}
