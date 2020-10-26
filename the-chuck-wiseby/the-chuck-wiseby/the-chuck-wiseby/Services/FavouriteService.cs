using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using Xamarin.Forms;

namespace the_chuck_wiseby.Services
{
    public class FavouriteService : IFavouriteService<ChuckJoke>
    {
        private readonly IDataStore<ChuckJoke> dataStore;

        private List<ChuckJoke> favourites;

        public FavouriteService(IDataStore<ChuckJoke> dataStore)
        {
            this.dataStore = dataStore;
            favourites = new List<ChuckJoke>();
            FavouriteCommand = new Command<ChuckJoke>(Save);
        }

        public ICommand FavouriteCommand { get; }

        public async void Initialize()
        {
            favourites.AddRange(await dataStore.Load());
        }

        public void Delete(ChuckJoke joke)
        {
            if (joke != null)
            {
                dataStore.Delete(joke);
                favourites.RemoveAll(item => item.Id == joke.Id);
            }
        }

        public IEnumerable<ChuckJoke> Get()
        {
            return favourites;
        }

        public void Save(ChuckJoke joke)
        {
            dataStore.Save(joke);
            favourites.Add(joke);
        }

        public bool IsFavourite(ChuckJoke joke)
        {
            var result = favourites.Where((favourite) => favourite.Id == joke.Id).FirstOrDefault();
            if (result != null) { return true; }
            else { return false; }
        }
    }
}
