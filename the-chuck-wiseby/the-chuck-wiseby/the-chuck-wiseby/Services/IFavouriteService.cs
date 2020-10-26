using System.Collections.Generic;
using System.Windows.Input;

namespace the_chuck_wiseby.Services
{
    public interface IFavouriteService<T>
    {
        ICommand FavouriteCommand { get; }
        void Initialize();
        IEnumerable<T> Get();
        void Save(T item);
        void Delete(T item);
        bool IsFavourite(T item);
    }
}
